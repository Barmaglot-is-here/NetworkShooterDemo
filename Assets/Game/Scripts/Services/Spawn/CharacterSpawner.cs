using Assets.Game.Scripts.Services;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Infrastructure.Spawn
{
    public class CharacterSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private SpawnPoint _spawnPointTeam1;
        [SerializeField]
        private SpawnPoint _spawnPointTeam2;

        [SerializeField]
        private CharacterUIBinder _uiBinder;

        public void Spawn(int team, ulong clientId)
        {
            var spawnPoint = team == 0 ? _spawnPointTeam1 : _spawnPointTeam2;

            var instance        = Object.Instantiate(_prefab, spawnPoint.Position, spawnPoint.Rotation);
            var networkObject   = instance.GetComponent<NetworkObject>();

            networkObject.SpawnAsPlayerObject(clientId);

            _uiBinder.Bind(instance);
        }
    }
}
