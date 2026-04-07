using Assets.Game.Scripts.Gameplay;
using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private HealthComponent _health;

        public void Bind(HealthComponent health)
        {
            _health = health;

            _health.Value.Changed += OnHealthChanged;

            OnHealthChanged(health.Value.Value);
        }

        private void OnHealthChanged(int value) 
            => _text.SetText(value.ToString());

        private void OnDestroy()
        {
            _health.Value.Changed -= OnHealthChanged;
        }
    }
}
