using Assets.Game.Scripts.Gameplay.Shooting;
using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class AmmoView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _ammoText;

        private WeaponClip _weaponClip;
        private AmmoPouch _ammoPouch;

        public void Bind(WeaponClip weaponClip, AmmoPouch ammoPouch)
        {
            _weaponClip = weaponClip;
            _ammoPouch  = ammoPouch;

            _weaponClip.AmmoCount.Changed += OnAmmoCountChanged;
            _ammoPouch.AmmoCount.Changed  += OnAmmoCountChanged;

            UpdateText(weaponClip, ammoPouch);
        }

        private void OnAmmoCountChanged(int value) => UpdateText(_weaponClip, _ammoPouch);

        private void UpdateText(WeaponClip weaponClip, AmmoPouch ammoPouch)
        {
            _ammoText.text = $"{weaponClip.AmmoCount.Value} / {ammoPouch.AmmoCount.Value}";
        }
    }
}
