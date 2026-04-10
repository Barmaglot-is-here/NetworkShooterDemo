using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Interaction
{
    public class Interactor : MonoBehaviour
    {
        [SerializeField]
        private float _radius = 2f;
        [SerializeField]
        private Transform _interactionSphereCenter;
        [SerializeField]
        private LayerMask _interactionMask;

        private InteractionTarget _currentTarget;

        private void Update()
        {
            var colliders = Physics.OverlapSphere(_interactionSphereCenter.position, _radius, _interactionMask);

            if (colliders.Length == 0)
            {
                UpdateTarget(null);

                return;
            }

            var closest         = GetClosest(colliders);
            var interactable    = closest.GetComponent<InteractionTarget>();

            if (_currentTarget != interactable)
                UpdateTarget(interactable);
        }

        private Collider GetClosest(Collider[] colliders)
        {
            float minSqr           = float.MaxValue;
            Collider closest    = null;

            foreach (var collider in colliders)
            {
                var sqr = (collider.transform.position - transform.position).sqrMagnitude;

                if (sqr < minSqr)
                {
                    minSqr = sqr;

                    closest = collider;
                }
            }

            return closest;
        }

        private void UpdateTarget(InteractionTarget newTarget)
        {
            _currentTarget?.SetFocusRpc(false);

            _currentTarget = newTarget;

            _currentTarget?.SetFocusRpc(true);
        }

        public void Interact() => _currentTarget?.Interact();
    }
}
