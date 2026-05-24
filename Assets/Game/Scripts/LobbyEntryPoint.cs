using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.Connection;
using Assets.Game.Scripts.Services.ProfileManagement;
using Assets.Game.Scripts.UI.Lobby;
using Assets.Game.Scripts.UI.Lobby.PlayerTab;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class LobbyEntryPoint : NetworkBehaviour
    {
        [SerializeField]
        private ServerSettings _serverSettings;

        [Space]
        [SerializeField]
        private ConnectionPopup _connectionPopup;
        [SerializeField]
        private LobbyPlayersTab _playersTab;
        [SerializeField]
        private ConnectionScreen _connectionScreen;

        private ConnectionService _connectionService;
        private PlayerList _playerList;
        private ProfileManager _profileManager;
        private PlayerTabService _playerTabService;
        private ClientConnectionHandler _clientConnectionHandler;
        private ServerConnectionHadler _serverConnectionHadler;

        private void Awake()
        {
            _connectionService          = new(_serverSettings);
            _playerList                 = new();
            _profileManager             = new();
            _playerTabService           = new(_playerList, _profileManager);
            _clientConnectionHandler    = new();

            _connectionPopup.Bind(_connectionService);
            _connectionScreen.Bind(_connectionService);
            _playersTab.Bind(_playerTabService);

            ApplicationServices.Add(_playerList);
            ApplicationServices.Add(_profileManager);
        }

        public override void OnNetworkSpawn()
        {
            if (IsServer)
                OnSerwerSpawn();
        }

        private void OnSerwerSpawn()
        {
            _serverConnectionHadler = new(_playerList, _profileManager);
        }

        public override void OnNetworkDespawn()
        {
            _clientConnectionHandler.Dispose();
            _serverConnectionHadler?.Dispose();
            _playerTabService.Dispose();
        }
    }
}
