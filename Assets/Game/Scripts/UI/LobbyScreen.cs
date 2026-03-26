using Assets.Game.Scripts.Services;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LobbyScreen : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _playerCount;

        [SerializeField]
        private Button _hostButton;
        [SerializeField]
        private Button _connectButton;
        [SerializeField]
        private Button _runButton;

        private LobbyService _lobby;

        private void Start()
        {
            _runButton.gameObject.SetActive(false);
        }

        public void Bind(LobbyService lobby)
        {
            _lobby = lobby;

            _lobby.OnClientCountChanged += OnClientCountChanged;


            //Temp
            _hostButton.onClick.Invoke();
            _runButton.onClick.Invoke();
        }

        private void OnClientCountChanged(int count)
            => _playerCount.SetText($"Player count: {count}");

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
        }

        private void OnHostButtonClick()
        {
            _lobby.Host();

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
            _lobby.TryConnect();

            _hostButton.gameObject.SetActive(false);
        }

        private void OnRunButtonClick()
        {
            _lobby.Run();

            gameObject.SetActive(false);
        }
    }
}
