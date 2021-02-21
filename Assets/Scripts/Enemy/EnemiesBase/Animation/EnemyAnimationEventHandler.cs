using System;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationEventHandler : MonoBehaviour
    {
        public Action OnAttackAnimationEnd;
        public Action OnStep;
        public Action OnAttack;

        public void HandleAnimationEvent(EnemyAnimationEventEnum p_eventName)
        {
            switch (p_eventName)
            {
                case EnemyAnimationEventEnum.ON_ATTACK_END:
                    OnAttackAnimationEnd?.Invoke();
                    break;
                case EnemyAnimationEventEnum.ON_STEP:
                    OnStep?.Invoke();
                    break;
                case EnemyAnimationEventEnum.ON_ATTACK:
                    OnAttack?.Invoke();
                    break;
            }
        }
    }
}