using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Game.Scripts.UI.WeaponSelection
{
    public class SlotClickHandler : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private bool _selectByDefault;

        private WeaponSlotView _slotView;

        public event Action<WeaponSlotView> OnClick;

        private void Awake()
        {
            _slotView = GetComponent<WeaponSlotView>();
        }

        private void Start()
        {
            if (_selectByDefault)
                OnClick.Invoke(_slotView);
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
            => OnClick.Invoke(_slotView);
    }
}
