using Assets.Game.Scripts.Services.Connection;
using System;
using UIManagement;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Lobby
{
    public class ConnectionPopup : UIScreen
    {
        [SerializeField]
        private Button _hostButton;
        [SerializeField]
        private Button _connectButton;
        [SerializeField]
        private Button _closeButton;

        private ConnectionService _connectionService;

        public void Bind(ConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        private void Awake()
        {
            _hostButton.onClick.AddListener(OnHostButtonClick);
            _connectButton.onClick.AddListener(OnConnectButtonClick);
            _closeButton.onClick.AddListener(OnCloseButtonClick);
        }

        private void OnHostButtonClick()
        {
            NetworkManager.Singleton.StartHost();

            ShowConnectionScreen();
        }

        private void OnConnectButtonClick()
        {
            if (_connectionService.TryConnect())
                ShowConnectionScreen();
            else
                ShowServerOverlflowMessage();
        }

        private void OnCloseButtonClick()
        {
            gameObject.SetActive(false);

            NetworkManager.Singleton.Shutdown();
        }

        private void ShowConnectionScreen()
        {
            gameObject.SetActive(false);

            UIManager.Show<ConnectionScreen>();
            UIManager.Hide<LobbyScreen>();
        }

        private void ShowServerOverlflowMessage()
        {
            throw new NotImplementedException();
        }
    }
}
