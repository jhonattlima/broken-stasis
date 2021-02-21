using UnityEngine;

namespace Player
{
    public class PlayerSoundColliderActivator
    {
        #region PRIVATE READONLY FIELDS
        private readonly Collider _lowSoundCollider;
        public readonly Collider _mediumSoundCollider;
        public readonly Collider _loudSoundCollider;
        #endregion PRIVATE READONLY FIELDS

        public PlayerSoundColliderActivator(Collider p_lowSoundCollider,
                                            Collider p_mediumSoundCollider,
                                            Collider p_loudSoundCollider)
        {
            _lowSoundCollider = p_lowSoundCollider;
            _mediumSoundCollider = p_mediumSoundCollider;
            _loudSoundCollider = p_loudSoundCollider;

            PlayerStatesManager.onStateChanged += HandleStateChanged;
        }

        private void HandleStateChanged(PlayerState p_playerState)
        {
            switch (p_playerState)
            {
                case PlayerState.RUNNING_FORWARD:
                case PlayerState.RUNNING_SIDEWAYS:
                    EnableMediumSoundCollider();
                    break;
                case PlayerState.WALKING_FORWARD:
                case PlayerState.WALKING_SIDEWAYS:
                case PlayerState.WALKING_BACKWARD:
                    EnableLowSoundCollider();
                    break;
                case PlayerState.STATIC:
                default:
                    DisableAllSoundColliders();
                    break;
            }
        }

        private void EnableLowSoundCollider()
        {
            _lowSoundCollider.enabled = true;

            _mediumSoundCollider.enabled = false;
            _loudSoundCollider.enabled = false;
        }

        private void EnableMediumSoundCollider()
        {
            _mediumSoundCollider.enabled = true;

            _lowSoundCollider.enabled = false;
            _loudSoundCollider.enabled = false;
        }

        private void EnableLoudSoundCollider()
        {
            _loudSoundCollider.enabled = true;

            _lowSoundCollider.enabled = false;
            _mediumSoundCollider.enabled = false;
        }

        private void DisableAllSoundColliders()
        {
            _lowSoundCollider.enabled = false;
            _mediumSoundCollider.enabled = false;
            _loudSoundCollider.enabled = false;
        }
    }
}
