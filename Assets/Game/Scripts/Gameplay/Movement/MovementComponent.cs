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

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (!IsOwner)
            return;

        MoveServerRpc(_direction);
        ApplyGravityServerRpc();
    }

    public void SetDirection(Vector2 direction) 
        => _direction = new(direction.x, 0, direction.y);

    [ServerRpc]
    public void JumpServerRpc()
    {
        if (!_characterController.isGrounded)
            return;

        _verticalVelocity = _jumpForce;
    }

    [ServerRpc]
    private void ApplyGravityServerRpc()
    {
        if (_characterController.isGrounded)
        {
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
