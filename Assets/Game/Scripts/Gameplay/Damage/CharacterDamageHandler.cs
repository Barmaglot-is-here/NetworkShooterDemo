using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Damage
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(DamageTarget))]
    public class CharacterDamageHandler : MonoBehaviour
    {
        private DamageTarget _damageTarget;
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _damageTarget       = GetComponent<DamageTarget>();
            _healthComponent    = GetComponent<HealthComponent>();
        }

        private void OnEnable()
        {
            _damageTarget.OnDamageTaken += OnDamageTaken;
        }

        private void OnDisable()
        {
            _damageTarget.OnDamageTaken -= OnDamageTaken;
        }

        private void OnDamageTaken(ulong senderId, int damage) 
            => _healthComponent.ApplyDamageRpc(damage);
    }
}
