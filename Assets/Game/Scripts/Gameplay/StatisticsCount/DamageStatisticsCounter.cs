using Assets.Game.Scripts.Gameplay.Damage;
using Assets.Game.Scripts.Services.StatisticsCount;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.StatisticsCount
{
    [RequireComponent(typeof(DamageTarget))]
    [RequireComponent(typeof(HealthComponent))]
    public class DamageStatisticsCounter : NetworkBehaviour
    {
        private DamageTarget _damageTarget;
        private HealthComponent _healthComponent;

        private void Awake()
        {
            _damageTarget       = GetComponent<DamageTarget>();
            _healthComponent    = GetComponent<HealthComponent>();
        }

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
            {
                enabled = false;

                return;
            }

            _damageTarget.OnDamageTaken += OnDamageTaken;
        }

        public override void OnNetworkDespawn()
        {
            _damageTarget.OnDamageTaken -= OnDamageTaken;
        }

        private void OnDamageTaken(ulong senderId, int damage)
        {
            StatisticsManager.Instance.CountDamage(OwnerClientId, senderId, damage);

            if (_healthComponent.Value <= 0)
                StatisticsManager.Instance.CountKill(OwnerClientId, senderId);
        }
    }
}
