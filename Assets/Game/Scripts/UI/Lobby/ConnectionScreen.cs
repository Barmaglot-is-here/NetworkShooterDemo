using Assets.Game.Scripts.Services.Connection;
using Assets.Game.Scripts.Services.SceneManagement;
using UIManagement;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Lobby
{
    public class ConnectionScreen : UIScreen
    {
        [SerializeField]
        private Button _cancelButton;
        [SerializeField]
        private Button _runButton;

        private ConnectionService _connectionService;

        public void Bind(ConnectionService connectionService)
        {
            _connectionService = connectionService;
        }

        private void Awake()
        {
            if (!NetworkManager.Singleton.IsServer)
                _runButton.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _cancelButton.onClick.AddListener(OnCancelButtonClick);
            _runButton.onClick.AddListener(OnRunButtonClick);
        }

        private void OnDisable()
        {
            _cancelButton.onClick.RemoveListener(OnCancelButtonClick);
            _runButton.onClick.RemoveListener(OnRunButtonClick);
        }

        private void OnCancelButtonClick()
        {
            _connectionService.Disconnect();

            Hide();

            UIManager.Show<LobbyScreen>();
        }

        private void OnRunButtonClick()
        {
            _cancelButton.gameObject.SetActive(false);
            _runButton.gameObject.SetActive(false);

            //UIManager.Show<LoadingScreen>();

            var sceneManager = NetworkManager.Singleton.SceneManager;

            sceneManager.LoadScene(SceneList.ARENA_1, LoadSceneMode.Single);
        }
    }
}
