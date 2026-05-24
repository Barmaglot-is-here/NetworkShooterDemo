using UnityEngine;

namespace Assets.Game.Scripts.UI.Controls
{
    public class LoadingIndicator : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _circle;

        [SerializeField]
        private float _rotationSpeed = 100;

        private float _rotation;

        private void Awake()
        {
            _circle.pivot = new(0.5f, 0.5f);

            var size        = _circle.sizeDelta;
            var position    = _circle.localPosition;
            
            _circle.localPosition = new(position.x + size.x / 2, position.y, position.x);
        }

        private void Update()
        {
            _rotation = (_rotation + _rotationSpeed * Time.deltaTime) % 360;

            _circle.localEulerAngles = new(0, 0, _rotation);
        }
    }
}
