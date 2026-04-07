using UnityEngine;

namespace Assets.Game.Scripts.UI.Controls
{
    public class LoadingCircle : MonoBehaviour
    {
        [SerializeField]
        private Transform _circle;

        [SerializeField]
        private float _rotationSpeed = 100;

        private float _rotation;

        private void Update()
        {
            _rotation = (_rotation + _rotationSpeed * Time.deltaTime) % 360;

            _circle.localEulerAngles = new(0, 0, _rotation);
        }
    }
}
