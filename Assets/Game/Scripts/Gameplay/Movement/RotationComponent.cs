using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class RotationComponent : NetworkBehaviour
{
    [SerializeField]
    private Transform _headTransform;

    [SerializeField]
    private float _maxAngleVertical = 15;
    [SerializeField]
    private float _minAngleVertical = -15;
    [SerializeField]
    private float _sencitivity = 2f;

    private float _verticalAngle;

    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();

        _verticalAngle = _headTransform.localEulerAngles.x;
    }

    [ServerRpc]
    public void RotateServerRpc(Vector2 direction)
    {
        RotateBody(direction.x);
        RotateHead(direction.y);
    }

    private void RotateBody(float angle)
    {
        var currentRotation = transform.rotation;

        angle = angle * Time.deltaTime * _sencitivity;
        transform.Rotate(0, angle, 0);
    }

    private void RotateHead(float angle)
    {
        angle = angle * Time.deltaTime * _sencitivity;

        _verticalAngle  = Mathf.Clamp(_verticalAngle - angle, _minAngleVertical, _maxAngleVertical);
        var rotation    = _headTransform.localEulerAngles;

        _headTransform.localEulerAngles = new(_verticalAngle, rotation.y, rotation.z);
    }
}
