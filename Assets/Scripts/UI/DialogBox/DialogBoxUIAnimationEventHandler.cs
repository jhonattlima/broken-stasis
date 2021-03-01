using System;
using UnityEngine;

namespace UI.Dialog
{
    [RequireComponent(typeof(Animator))]
    public class DialogBoxUIAnimationEventHandler : MonoBehaviour
    {
        public Action OnShowAnimationEnd;
        public Action OnHideAnimationEnd;

        public void HandleAnimationEvent(DialogBoxUIAnimationEventEnum p_eventName)
        {
            switch (p_eventName)
            {
                case DialogBoxUIAnimationEventEnum.ON_SHOW_END:
                    OnShowAnimationEnd?.Invoke();
                    break;
                case DialogBoxUIAnimationEventEnum.ON_HIDE_END:
                    OnHideAnimationEnd?.Invoke();
                    break;
            }
        }
    }
}
