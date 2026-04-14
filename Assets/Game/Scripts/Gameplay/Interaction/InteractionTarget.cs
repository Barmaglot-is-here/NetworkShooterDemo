using Assets.Game.Scripts.UI;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Interaction
{
    [RequireComponent(typeof(Outline))]
    public abstract class InteractionTarget : MonoBehaviour
    {
        private Outline _outline;
        private InteractionHint _interactionHint;

        private void Awake()
        {
            _outline            = GetComponent<Outline>();
            _interactionHint    = GetComponentInChildren<InteractionHint>(true);

            _outline.enabled = false;
        }

        public void SetFocus(bool state)
        {
            _outline.enabled = state;

            _interactionHint.SetActive(state);
        }

        public abstract void Interact();
    }
}
