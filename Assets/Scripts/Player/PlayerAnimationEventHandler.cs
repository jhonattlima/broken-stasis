using System;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationEventHandler : MonoBehaviour
    {
        public Action OnStep;

        public void HandleAnimationEvent(PlayerAnimationEventEnum p_eventName)
        {
            switch(p_eventName)
            {
                case PlayerAnimationEventEnum.ON_STEP:
                    OnStep?.Invoke();
                    break;
            }
        }
    }
}