using System;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Damage
{
    public class DamageTarget : NetworkBehaviour
    {
        private const string DAMAGE_OBJECT_TAG = "DamageObject";

        public event Action<int> OnDamageTaken;

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag(DAMAGE_OBJECT_TAG))
                return;

            var damageObject = collision.gameObject.GetComponent<DamageObject>();

            OnDamageTaken?.Invoke(damageObject.Damage);
        }
    }
}
