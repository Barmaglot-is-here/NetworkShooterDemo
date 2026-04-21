using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Lobby
{
    public class LobbyPlayerView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _name;

        public void Show(ulong playerId)
        {
            _name.text = playerId.ToString();
        }
    }
}
