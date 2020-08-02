using UnityEngine;

namespace Player
{
    public class PlayerAnimator
    {
        private const string STATIC = "Static";
        private const string WALKING = "Walking";
        private const string WALKING_BACKWARD = "Walking_Backward";
        private const string RUNNING = "Running";
        private const string DEAD = "Dead";
        private const string HIT = "Hit";

        private const string CROUCHING = "Crouching";

        private readonly Animator _animator;

        public PlayerAnimator(Animator p_animator)
        {
            _animator = p_animator;

            PlayerStatesManager.onStateChanged += HandleStateChanged;
            PlayerStatesManager.onPlayerCrouching += HandlePlayerCrouching;
        }

        private void HandlePlayerCrouching(bool p_crouching)
        {
            _animator.SetBool(CROUCHING, p_crouching);
        }

        private void HandleStateChanged(PlayerState p_playerState)
        {
            ResetAllVariables();

            switch(p_playerState)
            {
                case PlayerState.STATIC:
                    // _animator.SetTrigger(STATIC);
                    _animator.SetBool(STATIC, true);
                    break;
                case PlayerState.WALKING_FORWARD:
                case PlayerState.WALKING_SIDEWAYS:
                    // _animator.SetTrigger(WALKING);
                    _animator.SetBool(WALKING, true);
                    break;
                case PlayerState.WALKING_BACKWARD:
                    _animator.SetBool(WALKING_BACKWARD, true);
                    break;
                case PlayerState.RUNNING_FORWARD:
                case PlayerState.RUNNING_SIDEWAYS:
                    _animator.SetBool(RUNNING, true);
                    break;
                case PlayerState.DEAD:
                    _animator.SetTrigger(DEAD);
                    break;
                case PlayerState.HIT:
                    _animator.SetTrigger(HIT);
                    break;
            }
        }

        private void ResetAllVariables()
        {
            _animator.SetBool(STATIC, false);
            _animator.SetBool(WALKING, false);
            _animator.SetBool(WALKING_BACKWARD, false);
            _animator.SetBool(RUNNING, false);
        }
    }
}