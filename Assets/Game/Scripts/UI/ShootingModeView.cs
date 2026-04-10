using Assets.Game.Scripts.Gameplay.Shooting;
using Assets.Rx;
using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI
{
    public class ShootingModeView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        private Rx<ShootingMode> _shootingMode;

        public void Bind(Rx<ShootingMode> shootingMode)
        {
            _shootingMode = shootingMode;

            _shootingMode.Changed += OnShootingModeChanged;

            OnShootingModeChanged(_shootingMode.Value);
        }

        private void OnShootingModeChanged(ShootingMode mode)
        {
            var str = ShootingModeToString(mode);

            _text.SetText(str);
        }

        private string ShootingModeToString(ShootingMode mode)
        {
            string modeStr = mode switch
            {
                ShootingMode.Single => "1",
                ShootingMode.Multiple => "A",
                _ => throw new System.NotImplementedException(),
            };

            return modeStr;
        }

        private void OnDestroy()
        {
            if (_shootingMode != null)
                _shootingMode.Changed -= OnShootingModeChanged;
        }
    }
}
