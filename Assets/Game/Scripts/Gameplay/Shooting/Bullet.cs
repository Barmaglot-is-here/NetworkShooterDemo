using Assets.Game.Scripts.Gameplay.Damage;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Shooting
{
    [RequireComponent(typeof(Rigidbody))]
    public class Bullet : DamageObject
    {
        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void AddForce(Vector3 force) 
            => _rigidbody.AddRelativeForce(force, ForceMode.Impulse);

        private void OnTriggerEnter(Collider other)
            => Destroy(gameObject);
    }
}
