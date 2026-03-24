using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Shooting
{
    public class Weapon : MonoBehaviour
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

            var bullet = Object.Instantiate(_bulletPrefab, _shootPoint.position, transform.rotation);

            bullet.AddForce(Vector3.left * _shootForce);

            Clip.AmmoCount.Value--;
        }

        public void Reload()
        {

        }
    }
}
