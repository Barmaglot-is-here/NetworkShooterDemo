using Assets.Game.Scripts.Gameplay;
using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI.HUD
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private HealthComponent _health;

        public void Bind(HealthComponent health)
        {
            _health = health;

            _health.OnChanged += OnHealthChanged;

            OnHealthChanged(0, health.Value);
        }

        private void OnHealthChanged(int i, int value)
        {
            _text.SetText(value.ToString());
        }

        private void OnDestroy()
        {
            if (_health == null)
                return;

            _health.OnChanged -= OnHealthChanged;
        }
    }
}
