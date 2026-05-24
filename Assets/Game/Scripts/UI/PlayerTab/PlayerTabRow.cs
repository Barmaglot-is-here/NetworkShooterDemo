using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI.PlayerTab
{
    public class PlayerTabRow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _name;

        public void Show(string name)
        {
            _name.text = name;
        }
    }
}
