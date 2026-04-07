using UnityEngine;

namespace Assets.Game.Scripts.View
{
    public class CharacterSoundController : MonoBehaviour
    {
        [SerializeField]
        private AudioSource _audioFootsteps;
        [SerializeField]
        private AudioSource _landingAudio;

        private void OnFootstep(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
                _audioFootsteps.Play();
        }

        private void OnLand(AnimationEvent animationEvent)
        {
            if (animationEvent.animatorClipInfo.weight > 0.5f)
                _landingAudio.Play();
        }
    }
}
