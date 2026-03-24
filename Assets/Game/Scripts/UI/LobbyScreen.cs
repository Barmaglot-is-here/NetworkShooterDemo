using Assets.Game.Scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class LobbyScreen : MonoBehaviour
    {
        [SerializeField]
        private Button _hostButton;
        [SerializeField]
        private Button _connectButton;
        [SerializeField]
        private EntryPoint _entryPoint;

        private void OnEnable()
        {
            _hostButton.onClick.AddListener(OnHostButtonClick);
            _connectButton.onClick.AddListener(OnConnectButtonClick);
        }

        private void OnDisable()
        {
            _hostButton.onClick.RemoveListener(OnHostButtonClick);
            _connectButton.onClick.RemoveListener(OnConnectButtonClick);
        }

        private void OnHostButtonClick()
        {
            _entryPoint.Host();

            gameObject.SetActive(false);
        }

        private void OnConnectButtonClick()
        {
            _entryPoint.Connect();

            gameObject.SetActive(false);
        }
    }
}
