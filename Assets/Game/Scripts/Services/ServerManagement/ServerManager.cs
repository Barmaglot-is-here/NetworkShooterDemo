using Assets.Game.Scripts.Server.Spawn;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.StatisticsCount;
using Assets.Game.Scripts.Services.TeamManagement;
using System;
using Unity.Netcode;

namespace Assets.Game.Scripts.Server
{
    public class ServerManager
    {
        private readonly SynchronizationService _synchronizationService;
        private readonly CharacterSpawnService _spawnService;

        private int _maxPlayersCount;

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

        public static PlayerList PlayerList { get; }

        static ServerManager()
        {
            PlayerList = new();
        }

        public ServerManager(SynchronizationService synchronizationService, 
                             CharacterSpawnService spawnService,
                             int maxPlayersCount)
        {
            _synchronizationService = synchronizationService;
            _spawnService           = spawnService;
            _maxPlayersCount        = maxPlayersCount;

            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

            PlayerList.OnAdd += OnPlayerListAdd;
            PlayerList.OnRemove += OnPlayerListRemove;
        }

        private void OnPlayerListAdd(ulong playerId, int team)
            => StatisticsManager.Instance.AddPlayer(playerId);
        private void OnPlayerListRemove(ulong playerId, int team)
            => StatisticsManager.Instance.RemovePlayer(playerId);

        private void OnClientConnected(ulong id)
        {
            if (!NetworkManager.Singleton.IsServer)
                return;

            TeamDistributor.Distribute(PlayerList, id);
            _synchronizationService.SyncPlayerList(id);
        }

        private void OnClientDisconnected(ulong id) 
            => TeamDistributor.RemoveAndDistribute(PlayerList, id);

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
            _spawnService.Spawn(PlayerList.TeamOne, 0);
            _spawnService.Spawn(PlayerList.TeamTwo, 1);
        }
    }
}
