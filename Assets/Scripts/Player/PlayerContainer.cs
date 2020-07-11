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
    }
}

