using Assets.Rx;
using System.Collections;
using Unity.Netcode;
using UnityEngine;
using SM = Assets.Game.Scripts.Gameplay.Shooting.ShootingMode;

namespace Assets.Game.Scripts.Gameplay.Shooting
{
    public class Weapon : NetworkBehaviour
    {
        [SerializeField]
        private Bullet _bulletPrefab;
        [SerializeField]
        private float _shootForce;
        [SerializeField]
        private float _rpm;
        [SerializeField]
        private float _reloadTime;
        [SerializeField]
        private Transform _shootPoint;

        private WaitForSeconds _realodDelay;
        private WaitForSeconds _shootDelay;

        private Coroutine _realodCoroutine;
        private Coroutine _shootCoroutine;

        private bool _cancelShooting;

        public AmmoPouch Pouch { get; private set; }
        public WeaponClip Clip { get; private set; }
        public Rx<ShootingMode> ShootingMode { get; private set; }

        private void Awake()
        {
            Clip = new(30);

            _realodDelay    = new(_reloadTime);
            _shootDelay     = new(60 / _rpm);

            ShootingMode = new(SM.Multiple);
        }

        public void Connect(AmmoPouch ammoPouch)
        {
            Pouch = ammoPouch;
        }

        public void Shoot()
        {
            if (Clip.IsEmpty)
                return;

            _cancelShooting = false;

            _shootCoroutine ??= StartCoroutine(ShootCoroutine());
        }

        public void CancelShooting()
        {
            _cancelShooting = true;
        }

        public void SwitchShootingMode()
        {
            ShootingMode.Value = ShootingMode.Value == SM.Single ? SM.Multiple : SM.Single;
        }

        public void Reload()
        {
            if (_shootCoroutine != null)
                CancelShooting();

            _realodCoroutine ??= StartCoroutine(ReloadCoroutine());
        }

        [ServerRpc]
        private void SpawnBulletServerRpc()
        {
            var bullet          = Instantiate(_bulletPrefab, _shootPoint.position, transform.rotation);
            var networkObject   = bullet.GetComponent<NetworkObject>();

            networkObject.SpawnWithOwnership(OwnerClientId, true);
            bullet.AddForce(Vector3.forward * _shootForce);
        }

        private IEnumerator ShootCoroutine()
        {
            do
            {
                SpawnBulletServerRpc();

                Clip.Load(-1);

                yield return _shootDelay;
            } while (!Clip.IsEmpty && ShootingMode.Value == SM.Multiple && !_cancelShooting);

            _shootCoroutine = null;
        }

        private IEnumerator ReloadCoroutine()
        {
            yield return _realodDelay;

            int difference = Clip.AmmoCountMax - Clip.AmmoCount.Value;

            int ammo = Pouch.GetAmmo(difference);

            Clip.Load(ammo);

            _realodCoroutine = null;
        }
    }
}
