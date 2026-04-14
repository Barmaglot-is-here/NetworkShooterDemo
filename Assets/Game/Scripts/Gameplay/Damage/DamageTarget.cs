using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Damage
{
    public class DamageTarget : NetworkBehaviour
    {
        private const string DAMAGE_OBJECT_TAG = "DamageObject";

        public delegate void DamageDelegate(ulong senderId, int damage);

        public event DamageDelegate OnDamageTaken;

        private void OnCollisionEnter(Collision collision)
        {
            if (!collision.gameObject.CompareTag(DAMAGE_OBJECT_TAG))
                return;

            var damageObject = collision.gameObject.GetComponent<DamageObject>();

            OnDamageTaken?.Invoke(damageObject.OwnerClientId, damageObject.Damage);
        }
    }
}
