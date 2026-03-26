using Assets.Game.Scripts.Gameplay.GroundCheckSystem;
using Assets.Game.Scripts.View;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementComponent : NetworkBehaviour
{
    [SerializeField]
    private float _jumpForce = 3;
    [SerializeField]
    private float _movementSpeed = 20;
    [SerializeField]
    private float _gravity = -9.81f;

    private float _verticalVelocity;

    private Vector3 _direction;

    private CharacterController _characterController;
    private GroundCheckComponent _groundCheckComponent;
    private CharacterAnimationController _animationController;

    private bool IsGrounded => _groundCheckComponent.IsGrounded;

    private void Awake()
    {
        _characterController    = GetComponent<CharacterController>();
        _groundCheckComponent   = GetComponent<GroundCheckComponent>();
        _animationController    = GetComponent<CharacterAnimationController>();
    }

    private void Update()
    {
        if (!IsOwner)
            return;

        MoveServerRpc(_direction);
        ApplyGravityServerRpc();

        _animationController.SetGroundedState(IsGrounded);
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = new(direction.x, 0, direction.y);

        var speed = direction == Vector2.zero ? 0 : _movementSpeed;

        _animationController.SetMovementSpeed(speed);
    }

    [ServerRpc]
    public void JumpServerRpc()
    {
        if (!IsGrounded)
            return;

        _verticalVelocity = _jumpForce;

        _animationController.Jump();
    }

    [ServerRpc]
    private void ApplyGravityServerRpc()
    {
        if (_verticalVelocity < 0 && IsGrounded)
        {
            _animationController.JumpOver();

            _verticalVelocity = 0;

            return;
        }

        _verticalVelocity += _gravity * Time.deltaTime;

        var velocity    = _characterController.velocity;
        velocity        = new Vector3(0, _verticalVelocity, 0);

        _characterController.Move(velocity);
    }

    [ServerRpc]
    private void MoveServerRpc(Vector3 direction) 
        => _characterController.Move(transform.rotation * _direction * _movementSpeed * Time.deltaTime);
}
