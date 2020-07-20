using UnityEngine;

namespace Player
{
    public class PlayerAnimator
    {
        private const string STATIC = "Static";
        private const string WALKING = "Walking";
        private const string WALKING_BACKWARD = "Walking_Backward";
        private const string RUNNING = "Running";

        private readonly Animator _animator;

        public PlayerAnimator(Animator p_animator)
        {
            _animator = p_animator;

            PlayerStatesManager.onStateChanged += HandleStateChanged;
        }

        private void HandleStateChanged(PlayerState p_playerState)
        {
            ResetAllTriggers();

            switch(p_playerState)
            {
                case PlayerState.STATIC:
                    _animator.SetTrigger(STATIC);
                    break;
                case PlayerState.WALKING_FORWARD:
                case PlayerState.WALKING_SIDEWAYS:
                    _animator.SetTrigger(WALKING);
                    break;
                case PlayerState.WALKING_BACKWARD:
                    _animator.SetTrigger(WALKING_BACKWARD);
                    break;
                case PlayerState.RUNNING_FORWARD:
                case PlayerState.RUNNING_SIDEWAYS:
                    _animator.SetTrigger(RUNNING);
                    break;
            }
        }

        private void ResetAllTriggers()
        {
            _animator.ResetTrigger(STATIC);
            _animator.ResetTrigger(WALKING);
            _animator.ResetTrigger(WALKING_BACKWARD);
            _animator.ResetTrigger(RUNNING);
        }
    }
}