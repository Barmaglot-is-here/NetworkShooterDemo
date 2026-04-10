using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Damage
{
    [RequireComponent(typeof(HealthComponent))]
    [RequireComponent(typeof(DamageTarget))]
    public class CharacterDamageHandler : NetworkBehaviour
    {
        private DamageTarget _damageTarget;
        private HealthComponent _healthComponent;

        public override void OnNetworkSpawn()
        {
            _damageTarget       = GetComponent<DamageTarget>();
            _healthComponent    = GetComponent<HealthComponent>();

            _damageTarget.OnDamageTaken += OnDamageTaken;
        }

        public override void OnNetworkDespawn()
        {
            _damageTarget.OnDamageTaken -= OnDamageTaken;
        }

        private void OnDamageTaken(int value)
        {
            _healthComponent.ApplyDamageRpc(value);
        }
    }
}
