using Assets.Game.Scripts.Gameplay;
using Assets.Game.Scripts.Gameplay.DeathHandle;
using Assets.Game.Scripts.Gameplay.Shooting;
using Assets.Game.Scripts.UI.HUD;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Server.Spawn
{
    public class CharacterSpawnService : NetworkBehaviour
    {
        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private SpawnPoint _spawnPointTeam1;
        [SerializeField]
        private SpawnPoint _spawnPointTeam2;

        [SerializeField]
        private PlayerHUD _playerHUD;

        public void Spawn(IEnumerable<ulong> playerIds, int team)
        {
            foreach (var id in playerIds)
                SpawnRpc(id, team);
        }

        [Rpc(SendTo.Server)]
        public void SpawnRpc(ulong clientId, int team)
        {
            var spawnPoint = team == 0 ? _spawnPointTeam1 : _spawnPointTeam2;

            CreateView(spawnPoint, clientId);

            ClientRpcParams rpcParams = new();
            rpcParams.Send.TargetClientIds = new List<ulong> { clientId };

            SetupClientRpc(team, rpcParams);
        }

        [ClientRpc]
        private void SetupClientRpc(int team, ClientRpcParams rpcParams)
        {
            var client      = NetworkManager.Singleton.LocalClient;
            var character   = client.PlayerObject.gameObject;

            SetupCamera(character);
            SetupWeapon(character);
            SetupHUD(character);
            SetupDeathHandler(character, team);
        }

        private void CreateView(SpawnPoint spawnPoint, ulong clientId)
        {
            var instance        = Instantiate(_prefab, spawnPoint.Position, spawnPoint.Rotation);
            var networkObject   = instance.GetComponent<NetworkObject>();

            instance.name = instance.name.Replace("(Clone)", clientId.ToString());

            networkObject.SpawnAsPlayerObject(clientId);
        }

        private void SetupCamera(GameObject characer)
        {
            var cameraRoot      = characer.transform.Find("PlayerCameraRoot");
            var folowedCamera   = FindFirstObjectByType<FollowedCamera>();

            folowedCamera.SetTarget(cameraRoot);
        }

        private void SetupWeapon(GameObject character)
        {
            var weapon = character.GetComponentInChildren<Weapon>();

            weapon.Connect(new(120, 120));
        }

        private void SetupHUD(GameObject character)
        {
            var health = character.GetComponent<HealthComponent>();
            var weapon = character.GetComponentInChildren<Weapon>();

            _playerHUD.Bind(health, weapon);
        }

        private void SetupDeathHandler(GameObject character, int team)
        {
            var handler = character.GetComponent<DeathHandler>();

            handler.Setup(this, team);
        }
    }
}
