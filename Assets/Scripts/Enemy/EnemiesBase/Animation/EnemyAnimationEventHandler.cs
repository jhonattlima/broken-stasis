using System;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationEventHandler : MonoBehaviour
    {
        public Action OnAttackAnimationEnd;

        public void HandleAnimationEvent(EnemyAnimationEventEnum p_eventName)
        {
            OnAttackAnimationEnd?.Invoke();
        }
    }
}