using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.GroundCheckSystem
{
    public class GroundCheckComponent : MonoBehaviour
    {
        [SerializeField]
        private LayerMask _groundLayers;

        [SerializeField]
        private float GroundedOffset = -0.14f;
        [SerializeField]
        private float GroundedRadius = 0.28f;

        public bool IsGrounded { get; private set; }

        private void Update()
        {
            Vector3 spherePosition = new (transform.position.x, 
                                          transform.position.y - GroundedOffset,
                                          transform.position.z);
            IsGrounded = Physics.CheckSphere(spherePosition, GroundedRadius, _groundLayers,
                                             QueryTriggerInteraction.Ignore);
        }
    }
}
