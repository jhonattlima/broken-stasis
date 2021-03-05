using System;
using UnityEngine;

namespace UI.Minigame
{
    [RequireComponent(typeof(Animator))]
    public class MinigameUIAnimationEventHandler : MonoBehaviour
    {
        public Action OnShowAnimationEnd;
        public Action OnHideAnimationEnd;

        public void HandleAnimationEvent(MinigameUIAnimationEventEnum p_eventName)
        {
            switch (p_eventName)
            {
                case MinigameUIAnimationEventEnum.ON_SHOW_END:
                    OnShowAnimationEnd?.Invoke();
                    break;
                case MinigameUIAnimationEventEnum.ON_HIDE_END:
                    OnHideAnimationEnd?.Invoke();
                    break;
            }
        }
    }
}
