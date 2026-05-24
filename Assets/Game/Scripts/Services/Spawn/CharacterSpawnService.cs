using Assets.Game.Scripts.Gameplay;
using Assets.Game.Scripts.Gameplay.DeathHandle;
using Assets.Game.Scripts.Gameplay.Shooting;
using Assets.Game.Scripts.Services.Spawn;
using Assets.Game.Scripts.UI.HUD;
using Assets.Scripts.Gameplay;
using Signals;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Server.Spawn
{
    public class CharacterSpawnService : NetworkBehaviour
    {
        private const string CAMERA_ROOT_NAME = "PlayerCameraRoot";
        private const string WEAPON_ROOT_NAME = "WeaponRoot";

        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private SpawnPoint _spawnPointTeam1;
        [SerializeField]
        private SpawnPoint _spawnPointTeam2;

        [SerializeField]
        private PlayerHUD _playerHUD;

        private ReadySignal _readySignal;
        public IReadySignal ReadySignal => _readySignal;

        private void Awake()
        {
            _readySignal = new();
        }

        public override void OnNetworkSpawn() => _readySignal.SetReady();

        public void Spawn(IEnumerable<SpawnProfile> profiles)
        {
            foreach (var profile in profiles)
                SpawnRpc(profile);
        }

        [Rpc(SendTo.Server)]
        public void SpawnRpc(SpawnProfile profile)
        {
            var spawnPoint  = profile.Team == 0 ? _spawnPointTeam1 : _spawnPointTeam2;
            var character   = CreateView(spawnPoint, profile.PlayerId);

            CreateWeapon(character, profile.WeaponName, profile.PlayerId);

            ClientRpcParams rpcParams = new();
            rpcParams.Send.TargetClientIds = new List<ulong> { profile.PlayerId };

            SetupClientRpc(profile, rpcParams);
        }

        [ClientRpc]
        private void SetupClientRpc(SpawnProfile profile, ClientRpcParams rpcParams)
        {
            var client      = NetworkManager.Singleton.LocalClient;
            var character   = client.PlayerObject.gameObject;
            var weapon      = FindObjectsByType<Weapon>(FindObjectsSortMode.None)
                                .First(w => w.OwnerClientId == profile.PlayerId);

            //Temp
            weapon.Connect(new(120, 120));

            SetupCamera(character);
            SetupInput(character, weapon);
            SetupHUD(character, weapon);
            SetupDeathHandler(character, profile);
        }

        private GameObject CreateView(SpawnPoint spawnPoint, ulong clientId)
        {
            var instance        = Instantiate(_prefab, spawnPoint.Position, spawnPoint.Rotation);
            var networkObject   = instance.GetComponent<NetworkObject>();

            instance.name = instance.name.Replace("(Clone)", clientId.ToString());

            networkObject.SpawnAsPlayerObject(clientId);

            return instance;
        }

        private void CreateWeapon(GameObject character, string prefabName, ulong ownerId)
        {
            var weaponTargetPoint  = GetWeaponSpawnPoint(character);
            var weapon      = Resources.Load<Weapon>(prefabName);

            var instance        = Instantiate(weapon);
            var followedObject  = instance.GetComponent<FollowedObject>();
            var networkObject   = instance.GetComponent<NetworkObject>();

            followedObject.SetTarget(weaponTargetPoint);
            networkObject.SpawnWithOwnership(ownerId);
        }

        private void SetupCamera(GameObject characer)
        {
            var cameraRoot      = characer.transform.Find(CAMERA_ROOT_NAME);
            var folowedCamera   = Camera.main.GetComponent<FollowedObject>();

            folowedCamera.SetTarget(cameraRoot);
        }

        private void SetupInput(GameObject character, Weapon weapon)
        {
            var inputManager = character.GetComponent<PlayerInputManager>();

            inputManager.Setup(weapon);
        }

        private void SetupHUD(GameObject character, Weapon weapon)
        {
            var health = character.GetComponent<HealthComponent>();

            _playerHUD.Bind(health, weapon);
        }

        private void SetupDeathHandler(GameObject character, SpawnProfile spawnProfile)
        {
            var handler = character.GetComponent<DeathHandler>();

            handler.Setup(this, spawnProfile);
        }

        private Transform GetWeaponSpawnPoint(GameObject character)
            => character.transform
                .Find(CAMERA_ROOT_NAME)
                .Find(WEAPON_ROOT_NAME);
    }
}
