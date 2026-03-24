using System;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Damage
{
    public class DamageTarget : MonoBehaviour
    {
        private const string DAMAGE_OBJECT_TAG = "DamageObject";

        public event Action<int> OnDamageTaken;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(DAMAGE_OBJECT_TAG))
                return;

            var damageObject = other.GetComponent<DamageObject>();

            OnDamageTaken.Invoke(damageObject.Damage);
        }
    }
}
