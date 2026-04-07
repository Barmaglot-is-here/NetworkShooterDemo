using Assets.Game.Scripts.Server;
using Assets.Game.Scripts.UI.Controls;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LobbyScreen : NetworkBehaviour
    {
        [SerializeField]
        private TMP_Text _playersCount;
        
        [SerializeField]
        private Button _hostButton;
        [SerializeField]
        private Button _connectButton;
        [SerializeField]
        private Button _runButton;

        [SerializeField]
        private GameObject _loadingIndicator;

        private ServerManager _connectionService;

        private void Start()
        {
            _runButton.gameObject.SetActive(false);
            ShowPlayerCount(false);
        }

        public void Bind(ServerManager connectionService)
        {
            _connectionService = connectionService;

            _connectionService.OnClientCountChanged += OnClientCountChanged;
        }

        private void OnClientCountChanged(int count)
            => _playersCount.SetText($"Players count: {count}");

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
            _connectionService.Host();

            HideHostButtons();

            _runButton.gameObject.SetActive(true);

            ShowPlayerCount(true);
        }

        private void HideHostButtons()
        {
            _hostButton.gameObject.SetActive(false);
            _connectButton.gameObject.SetActive(false);
        }

        private void OnConnectButtonClick()
        {
            _connectionService.TryConnect();

            HideHostButtons();

            _loadingIndicator.SetActive(true);
            ShowPlayerCount(true);
        }

        private void ShowPlayerCount(bool state)
        {
            _playersCount.transform.parent.gameObject.SetActive(state);
        }

        private void OnRunButtonClick()
        {
            _connectionService.Run();
            HideClientRpc();
        }

        [ClientRpc]
        private void HideClientRpc() => gameObject.SetActive(false);
    }
}
