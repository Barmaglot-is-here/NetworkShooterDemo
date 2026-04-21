using Assets.Game.Scripts.Server;
using Assets.Game.Scripts.Services.TeamManagement;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LobbyScreen : NetworkBehaviour
    {
        [SerializeField]
        private Button _hostButton;
        [SerializeField]
        private Button _connectButton;
        [SerializeField]
        private Button _runButton;

        [SerializeField]
        private LobbyPlayersTab _lobbyPlayersTab;
        [SerializeField]
        private GameObject _loadingIndicator;

        private ServerManager _serverManager;

        private void Start()
        {
            _runButton.gameObject.SetActive(false);
        }

        public void Bind(ServerManager serverManager)
        {
            _serverManager = serverManager;
        }

        private void OnEnable()
        {
            _hostButton.onClick.AddListener(OnHostButtonClick);
            _connectButton.onClick.AddListener(OnConnectButtonClick);
            _runButton.onClick.AddListener(OnRunButtonClick);
        }

        private void OnDisable()
        {
            _hostButton.onClick.RemoveListener(OnHostButtonClick);
            _connectButton.onClick.RemoveListener(OnConnectButtonClick);
            _runButton.onClick.RemoveListener(OnRunButtonClick);
        }

        private void OnHostButtonClick()
        {
            _serverManager.Host();

            HideHostButtons();

            _runButton.gameObject.SetActive(true);
        }

        private void HideHostButtons()
        {
            _hostButton.gameObject.SetActive(false);
            _connectButton.gameObject.SetActive(false);
        }

        private void OnConnectButtonClick()
        {
            _serverManager.TryConnect();

            HideHostButtons();

            _loadingIndicator.SetActive(true);
        }

        private void OnRunButtonClick()
        {
            _serverManager.Run();
            HideClientRpc();
        }

        [ClientRpc]
        private void HideClientRpc() => gameObject.SetActive(false);
    }
}
