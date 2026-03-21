using System;
using Unity.Netcode;
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
            NetworkManager.Singleton.StartHost();

            gameObject.SetActive(false);
        }

        private void OnConnectButtonClick()
        {
            NetworkManager.Singleton.StartClient();

            gameObject.SetActive(false);
        }
    }
}
