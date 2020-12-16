using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class NotificationUIAnimationEventHandler : MonoBehaviour
{
    public Action OnShowAnimationEnd;
    public Action OnHideAnimationEnd;

    public void HandleAnimationEvent(NotificationUIAnimationEventEnum p_eventName)
    {
        switch(p_eventName)
        {
            case NotificationUIAnimationEventEnum.ON_SHOW_END:
                OnShowAnimationEnd?.Invoke();
                break;
            case NotificationUIAnimationEventEnum.ON_HIDE_END:
                OnHideAnimationEnd?.Invoke();
                break;
        }
    }
}
