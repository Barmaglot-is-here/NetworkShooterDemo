using UnityEngine;

namespace Assets.Game.Scripts.View
{
    [RequireComponent(typeof(Animator))]
    public class CharacterAnimationController : MonoBehaviour
    {
        [SerializeField]
        private float _animationTransitionSpeed = 0.2f;

        private Animator _animator;

        private float _animationBlend;
        private float _targetSpeed;
        private float _animationTransitionTime;

        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDMotionSpeed;

        private void Awake()
        {
            _animator = GetComponent<Animator>();

            _animIDSpeed        = Animator.StringToHash("Speed");
            _animIDGrounded     = Animator.StringToHash("Grounded");
            _animIDJump         = Animator.StringToHash("Jump");
            _animIDMotionSpeed  = Animator.StringToHash("MotionSpeed");
        }

        public void SetMovementSpeed(float speed)
        {
            _animator.SetFloat(_animIDMotionSpeed, speed == 0 ? 0 : 1);

            _targetSpeed                = speed;
            _animationTransitionTime    = 0;
        }

        public void JumpOver()
        {
            _animator.SetBool(_animIDJump, false);
        }

        private void Update() => UpdateMovement();

        private void UpdateMovement()
        {
            _animationTransitionTime += _animationTransitionSpeed * Time.deltaTime;

            _animationBlend = Mathf.Lerp(_animationBlend, _targetSpeed, _animationTransitionTime);

            if (_animationBlend < 0.01f)
                _animationBlend = 0f;

            _animator.SetFloat(_animIDSpeed, _animationBlend);
        }

        public void Jump()
        {
            _animator.SetBool(_animIDJump, true);
        }

        public void SetGroundedState(bool state)
        {
            _animator.SetBool(_animIDGrounded, state);
        }
    }
}
