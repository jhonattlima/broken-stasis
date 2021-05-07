using System;
using UnityEngine;

namespace Gameplay.Enemy.EnemiesBase
{
    [RequireComponent(typeof(Animator))]
    public class EnemyAnimationEventHandler : MonoBehaviour
    {
        public Action OnAttackAnimationEnd;
        public Action OnStep;
        public Action OnAttack;
        public Action OnAwoken;

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
                case EnemyAnimationEventEnum.ON_AWOKEN:
                    OnAwoken?.Invoke();
                    break;
            }
        }
    }
}