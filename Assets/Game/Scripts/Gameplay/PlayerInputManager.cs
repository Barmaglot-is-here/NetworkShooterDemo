using Assets.Game.Scripts.Gameplay.Shooting;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Gameplay
{
    public class PlayerInputManager : NetworkBehaviour
    {
        [SerializeField]
        private MovementComponent _movementComponent;
        [SerializeField]
        private RotationComponent _rotationComponent;
        [SerializeField]
        private Weapon _weapon;

        private InputActions _inputActions;

        private InputActions.PlayerActions PlayerActions => _inputActions.Player;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        public override void OnNetworkSpawn()
        {
            if (!IsOwner)
                return;

            _inputActions = new();

            _inputActions.Enable();

            PlayerActions.Move.performed += OnMovePerformed;
            PlayerActions.Move.canceled += OnMoveCanceled;
            PlayerActions.Jump.performed += OnJumpPerformed;
            PlayerActions.Look.performed += OnLookPerformed;
            PlayerActions.Attack.performed += OnAtackPerformed;
        }


        public override void OnNetworkDespawn()
        {
            if (!IsOwner)
                return;

            _inputActions.Disable();

            PlayerActions.Move.performed -= OnMovePerformed;
            PlayerActions.Move.canceled -= OnMoveCanceled;
            PlayerActions.Jump.performed -= OnJumpPerformed;
            PlayerActions.Look.performed -= OnLookPerformed;
            PlayerActions.Attack.performed -= OnAtackPerformed;
        }

        private void OnMovePerformed(InputAction.CallbackContext context) 
            => _movementComponent.SetDirection(context.ReadValue<Vector2>());

        private void OnMoveCanceled(InputAction.CallbackContext context)
            => _movementComponent.SetDirection(Vector2.zero);

        private void OnJumpPerformed(InputAction.CallbackContext context)
            => _movementComponent.JumpServerRpc();

        private void OnLookPerformed(InputAction.CallbackContext context)
            => _rotationComponent.RotateServerRpc(context.ReadValue<Vector2>());

        private void OnAtackPerformed(InputAction.CallbackContext context)
            => _weapon.Shoot();
    }
}
