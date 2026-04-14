using Assets.Game.Scripts.UI.Statsistic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Game.Scripts.UI
{
    public class UIInputManager : NetworkBehaviour
    {
        [SerializeField]
        private StatisticsTab _statisticsTab;

        private InputActions _actions;
        private InputActions.UIActions UIActions => _actions.UI;

        public override void OnNetworkSpawn()
        {
            _actions = new();
            _actions.Enable();

            UIActions.ShowStats.performed   += OnShowStatsPerformed;
            UIActions.ShowStats.canceled    += OnShowStatsCanceled;
        }

        private void OnShowStatsPerformed(InputAction.CallbackContext context)
            => _statisticsTab.Show();
        private void OnShowStatsCanceled(InputAction.CallbackContext context)
            => _statisticsTab.Hide();

        public override void OnNetworkDespawn()
        {
            if (!IsOwner)
                return;

            _actions.Disable();

            UIActions.ShowStats.performed   -= OnShowStatsPerformed;
            UIActions.ShowStats.canceled    -= OnShowStatsCanceled;
        }
    }
}
