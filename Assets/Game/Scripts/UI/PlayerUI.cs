using Assets.Game.Scripts.Gameplay;
using Assets.Game.Scripts.Gameplay.Shooting;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField]
        private HealthView _healthView;
        [SerializeField]
        private AmmoView _ammoView;

        public void Bind(GameObject character)
        {
            var health = character.GetComponent<HealthComponent>();
            var weapon = character.GetComponentInChildren<Weapon>();

            _healthView.Bind(health);
            _ammoView.Bind(weapon.Clip, new(30, 60));
        }
    }
}
