using Assets.Game.Scripts.Gameplay.Shooting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Game.Scripts.UI.WeaponSelection
{
    public class WeaponSlotView : MonoBehaviour
    {
        [SerializeField]
        private GameObject _checkmark;
        [SerializeField]
        private Image _frame;
        [SerializeField]
        private Image _icon;

        [SerializeField]
        private Color _defaultColor;
        [SerializeField]
        private Color _selectionColor;

        [field: SerializeField]
        public Weapon Weapon { get; private set; }

        public void Select()
        {
            _checkmark.SetActive(true);
            _frame.color = _selectionColor;
        }

        public void Deselect()
        {
            _checkmark.SetActive(false);
            _frame.color = _defaultColor;
        }

        private void OnValidate()
        {
            if (Weapon != null)
                _icon.sprite = Weapon.Icon;
        }
    }
}
