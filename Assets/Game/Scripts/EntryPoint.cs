using Assets.Game.Scripts.Infrastructure.Spawn;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private CharacterSpawner _characterSpawner;

        public void Host()
        {
            NetworkManager.Singleton.StartHost();
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

            _characterSpawner.Spawn(0, NetworkManager.Singleton.LocalClientId);
        }

        private void OnClientConnected(ulong clientId) 
            => _characterSpawner.Spawn(1, clientId);

        public void Connect() => NetworkManager.Singleton.StartClient();
    }
}
