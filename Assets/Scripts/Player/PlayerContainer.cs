using UnityEngine;

namespace Player
{
    public class PlayerContainer : MonoBehaviour
    {
        [Header("Movement Reference")]
        [Space(5)]
        public CharacterController characterController;
        public Transform playerTransform;

        [Header("Animator")]
        [Space(5)]
        public Animator playerAnimator;

        [Header("Sound Colliders")]
        [Space(5)]
        public Collider lowSoundCollider;
        public Collider mediumSoundCollider;
        public Collider loudSoundCollider;

        private void Awake()
        {
            if (characterController == null)
                throw new MissingComponentException("CharacterController not found in PlayerContainer!");
            if (playerTransform == null)
                throw new MissingComponentException("PlayerTransform not found in PlayerContainer!");
            if (playerAnimator == null)
                throw new MissingComponentException("PlayerAnimator not found in PlayerContainer!");
            if (lowSoundCollider == null)
                throw new MissingComponentException("LowSoundCollider not found in PlayerContainer!");
            if (mediumSoundCollider == null)
                throw new MissingComponentException("MediumSoundCollider not found in PlayerContainer!");
            if (loudSoundCollider == null)
                throw new MissingComponentException("LoudSoundCollider not found in PlayerContainer!");
        }
    }
}

