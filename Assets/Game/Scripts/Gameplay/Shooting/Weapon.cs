using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Shooting
{
    public class Weapon : NetworkBehaviour
    {
        [SerializeField]
        private Bullet _bulletPrefab;
        [SerializeField]
        private float _shootForce;
        [SerializeField]
        private Transform _shootPoint;

        private AmmoPouch _ammoPouch;

        public WeaponClip Clip { get; private set; }

        private void Awake()
        {
            Clip = new(30);
        }

        public void Connect(AmmoPouch ammoPouch)
        {
            _ammoPouch = ammoPouch;
        }

        public void Shoot()
        {
            if (Clip.IsEmpty)
                return;

            SpawnBulletServerRpc();

            Clip.AmmoCount.Value--;
        }

        [ServerRpc]
        private void SpawnBulletServerRpc()
        {
            var bullet          = Instantiate(_bulletPrefab, _shootPoint.position, transform.rotation);
            var networkObject   = bullet.GetComponent<NetworkObject>();

            networkObject.SpawnWithOwnership(OwnerClientId, true);
            bullet.AddForce(Vector3.forward * _shootForce);
        }

        public void Reload()
        {

        }
    }
}
