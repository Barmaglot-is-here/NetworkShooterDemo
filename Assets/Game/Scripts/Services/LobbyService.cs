using Assets.Game.Scripts.Infrastructure.Spawn;
using System;
using Unity.Netcode;

namespace Assets.Game.Scripts.Services
{
    public class LobbyService
    {
        private readonly CharacterSpawnService _spawnService;

        private int _maxPlayers;

        public event Action<int> OnClientCountChanged;

        public int PlayersCount => NetworkManager.Singleton.ConnectedClientsIds.Count;
        public int MaxPlayersCount 
        {
            get => _maxPlayers;
            set
            {
                if (value % 2 != 0)
                    throw new ArgumentException("The maximum number of players must be divisible by 2");

                _maxPlayers = value;
            }
        }

        public LobbyService(CharacterSpawnService spawnService)
        {
            _spawnService = spawnService;

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
            if (PlayersCount < MaxPlayersCount)
                return false;

            NetworkManager.Singleton.StartClient();
                
            return true;
        }

        public void Run()
        {
            foreach (var clientId in NetworkManager.Singleton.ConnectedClientsIds)
                _spawnService.Spawn(0, clientId);
        }
    }
}
