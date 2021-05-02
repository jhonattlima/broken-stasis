using Gameplay.Player.Animation;
using Gameplay.Player.Item;
using Gameplay.Player.Motion;
using UnityEngine;

namespace Gameplay.Player
{
    [System.Serializable]
    public struct PlayerSuitData
    {
        public PlayerSuitEnum suitType;
        public GameObject suitGameObject;
        public Animator suitAnimator;
        public PlayerAnimationEventHandler suitAnimationEventHandler;
    }
    public class PlayerContainer : MonoBehaviour
    {
        [Header("Movement Reference")]
        [Space(5)]
        public CharacterController characterController;
        public Transform playerTransform;
        public PlayerTunnelBehaviour playerTunnelBehaviour;

        [Header("Suits")]
        [Space(5)]
        public PlayerSuitData[] suits;

        [Header("Lights")]
        [Space(5)]
        public Light[] playerLights;
        public GameObject playerIlluminationGameObject;

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
            if (suits == null)
                throw new MissingComponentException("PlayerSuits not found in PlayerContainer!");
            if (lowSoundCollider == null)
                throw new MissingComponentException("LowSoundCollider not found in PlayerContainer!");
            if (mediumSoundCollider == null)
                throw new MissingComponentException("MediumSoundCollider not found in PlayerContainer!");
            if (loudSoundCollider == null)
                throw new MissingComponentException("LoudSoundCollider not found in PlayerContainer!");
        }
    }
}

