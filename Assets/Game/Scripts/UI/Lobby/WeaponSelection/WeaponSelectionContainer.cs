using Assets.Game.Scripts.Gameplay.Shooting;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.UI.WeaponSelection
{
    public class WeaponSelectionContainer : MonoBehaviour
    {
        private SlotClickHandler[] _clickHandlers;

        private WeaponSlotView _selectedView;

        public event Action<GameObject> OnWeaponSelected;

        private void Awake()
        {
            _clickHandlers = GetComponentsInChildren<SlotClickHandler>();

            foreach (var handler in _clickHandlers)
                Setup(handler);
        }

        private void Setup(SlotClickHandler handler) => handler.OnClick += OnSlotClick;

        private void OnSlotClick(WeaponSlotView view)
        {
            _selectedView?.Deselect();
            view.Select();

            _selectedView = view;

            OnWeaponSelected.Invoke(view.Weapon.gameObject);
        }
    }
}
