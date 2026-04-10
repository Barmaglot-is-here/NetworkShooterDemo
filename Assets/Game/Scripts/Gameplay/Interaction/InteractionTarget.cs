using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Interaction
{
    [RequireComponent(typeof(Outline))]
    [RequireComponent(typeof(NetworkObject))]
    public abstract class InteractionTarget : NetworkBehaviour
    {
        private Outline _outline;

        private void Awake()
        {
            _outline = GetComponent<Outline>();

            _outline.enabled = false;
        }

        [Rpc(SendTo.Owner)]
        public void SetFocusRpc(bool state)
        {
            _outline.enabled = state;
        }

        public abstract void Interact();
    }
}
