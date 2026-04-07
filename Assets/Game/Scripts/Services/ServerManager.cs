using Assets.Game.Scripts.Server.Spawn;
using System;
using Unity.Netcode;

namespace Assets.Game.Scripts.Server
{
    public class ServerManager
    {
        private readonly CharacterSpawnService _spawnService;

        private int _maxPlayersCount;

        public event Action<int> OnClientCountChanged;

        public int PlayersCount => NetworkManager.Singleton.ConnectedClientsIds.Count;
        public int MaxPlayersCount 
        {
            get => _maxPlayersCount;
            set
            {
                if (value % 2 != 0)
                    throw new ArgumentException("The maximum number of players must be divisible by 2");

                _maxPlayersCount = value;
            }
        }

        public ServerManager(CharacterSpawnService spawnService, int maxPlayersCount)
        {
            _spawnService       = spawnService;
            _maxPlayersCount    = maxPlayersCount;

            NetworkManager.Singleton.OnClientConnectedCallback += OnConnectionEventPerformed;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnConnectionEventPerformed;
        }

        private void OnConnectionEventPerformed(ulong obj)
        {
            OnClientCountChanged.Invoke(PlayersCount);
        }

        public void Host() => NetworkManager.Singleton.StartHost();
        public bool TryConnect()
        {
            if (PlayersCount >= MaxPlayersCount)
                return false;

            NetworkManager.Singleton.StartClient();

            return true;
        }

        public void Run()
        {
            foreach (var clientId in NetworkManager.Singleton.ConnectedClientsIds)
                _spawnService.SpawnServerRpc(0, clientId);
            
            _spawnService.SetupClientRpc();
        }
    }
}
