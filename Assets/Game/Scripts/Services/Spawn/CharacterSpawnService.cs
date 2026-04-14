using Assets.Game.Scripts.Gameplay;
using Assets.Game.Scripts.Gameplay.Shooting;
using Assets.Game.Scripts.UI.HUD;
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

        [ServerRpc]
        public void SpawnServerRpc(int team, ulong clientId)
        {
            var spawnPoint  = team == 0 ? _spawnPointTeam1 : _spawnPointTeam2;
            var character   = CreateView(spawnPoint, clientId);
        }

        [ClientRpc]
        public void SetupClientRpc()
        {
            var client      = NetworkManager.Singleton.LocalClient;
            var character   = client.PlayerObject.gameObject;

            SetupCamera(character);
            SetupHUD(character);
        }

        private GameObject CreateView(SpawnPoint spawnPoint, ulong clientId)
        {
            var instance        = Instantiate(_prefab, spawnPoint.Position, spawnPoint.Rotation);
            var networkObject   = instance.GetComponent<NetworkObject>();

            instance.name = instance.name.Replace("(Clone)", clientId.ToString());

            networkObject.SpawnAsPlayerObject(clientId);

            return instance;
        }

        private void SetupCamera(GameObject characer)
        {
            var cameraRoot      = characer.transform.Find("PlayerCameraRoot");
            var folowedCamera   = FindFirstObjectByType<FollowedCamera>();

            folowedCamera.SetTarget(cameraRoot);
        }

        private void SetupHUD(GameObject character)
        {
            var health = character.GetComponent<HealthComponent>();
            var weapon = character.GetComponentInChildren<Weapon>();

            _playerHUD.Bind(health, weapon);
        }
    }
}
