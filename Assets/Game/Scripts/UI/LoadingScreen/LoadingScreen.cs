using Assets.Game.Scripts.UI.PlayerTab;
using UIManagement;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Game.Scripts.UI
{
    internal class LoadingScreen : UIScreen
    {
        [SerializeField]
        private PlayerTabController _playerTabController;

        private PlayerTabService _playerTabService;

        private void Awake()
        {

            var sceneManager = NetworkManager.Singleton.SceneManager;
            sceneManager.OnLoad += OnSceneLoad;
        }

        public void Bind(PlayerTabService playerTabService)
        {
            _playerTabService = playerTabService;

        }

        private void OnSceneLoad(ulong clientId, string sceneName, LoadSceneMode loadSceneMode, AsyncOperation asyncOperation)
        {
            Debug.Log(clientId);
        }
    }
}
