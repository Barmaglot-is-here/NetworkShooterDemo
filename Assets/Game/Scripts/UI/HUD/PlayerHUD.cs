using Assets.Game.Scripts.Gameplay;
using Assets.Game.Scripts.Gameplay.Shooting;
using UnityEngine;

namespace Assets.Game.Scripts.UI.HUD
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField]
        private HealthView _healthView;
        [SerializeField]
        private AmmoView _ammoView;
        [SerializeField]
        private ShootingModeView _shootingModeView;

        public void Bind(HealthComponent health, Weapon weapon)
        {
            _healthView.Bind(health);
            _ammoView.Bind(weapon.Clip, weapon.Pouch);
            _shootingModeView.Bind(weapon.ShootingMode);
        }
    }
}
