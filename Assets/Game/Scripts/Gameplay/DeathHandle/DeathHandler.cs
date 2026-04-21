using Assets.Game.Scripts.Server.Spawn;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.DeathHandle
{
    [RequireComponent(typeof(HealthComponent))]
    public class DeathHandler : NetworkBehaviour
    {
        private HealthComponent _healthComponent;
        private CharacterSpawnService _spawnService;

        private int _team;

        public void Setup(CharacterSpawnService spawnService, int team)
        {
            _healthComponent    = GetComponent<HealthComponent>();
            _spawnService       = spawnService;
            _team               = team;

            _healthComponent.OnChanged += OnHealthChanged;
        }

        public override void OnNetworkDespawn()
        {
            if (_healthComponent != null)
                _healthComponent.OnChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int previousValue, int newValue)
        {
            if (newValue <= 0)
                OnDeath();
        }

        private void OnDeath()
        {
            _spawnService.SpawnRpc(OwnerClientId, _team);

            DestroyRpc();
        }

        [Rpc(SendTo.Server)]
        private void DestroyRpc() => this.SafeDestroy();
    }
}
