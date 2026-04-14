using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class InteractionHint : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private void Awake()
        {
            var actions = new InputActions();
            var keyName = actions.Player.Interact.controls[0].name;
            
            _text.text = keyName;
        }

        public void SetActive(bool state) => gameObject.SetActive(state);
    }
}
