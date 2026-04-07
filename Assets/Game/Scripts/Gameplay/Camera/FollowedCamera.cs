using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Camera
{
    public class FollowedCamera : MonoBehaviour
    {
        private Transform _target;

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            transform.position = _target.position;
            transform.rotation = _target.rotation;
        }
    }
}
