using UIManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.Lobby
{
    public class LobbyScreen : UIScreen
    {
        [SerializeField]
        private Button _playButton;

        private void Awake()
        {
            _playButton.onClick.AddListener(OnPlayButtonClick);
        }

        private void OnPlayButtonClick() => UIManager.Show<ConnectionPopup>();
    }
}
