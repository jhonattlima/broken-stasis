using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyAnimator
    {
        private const string IDLE = "Idle";
        private const string WALKING = "Walking";
        private const string RUNNING = "Running";
        private const string ATTACKING = "Attacking";

        private EnemyStatesManager _stateManager;
        private Animator _animator;

        public EnemyAnimator(EnemyStatesManager p_stateManager, Animator p_animator)
        {
            _stateManager = p_stateManager;
            _animator = p_animator;

            _stateManager.onStateChanged += HandleStateChanged;
        }

        private void HandleStateChanged(EnemyState p_enemyState)
        {
            ResetAllTriggers();

            switch(p_enemyState)
            {
                case EnemyState.IDLE:
                    _animator.SetTrigger(IDLE);
                    break;
                case EnemyState.INVESTIGATING:
                case EnemyState.PATROLLING:
                    _animator.SetTrigger(WALKING);
                    break;
                case EnemyState.RUNNING:
                    _animator.SetTrigger(RUNNING);
                    break;
                case EnemyState.ATTACKING:
                    _animator.SetTrigger(ATTACKING);
                    break; 
            }
        }

        private void ResetAllTriggers()
        {
            _animator.ResetTrigger(IDLE);
            _animator.ResetTrigger(WALKING);
            _animator.ResetTrigger(RUNNING);
            _animator.ResetTrigger(ATTACKING);
        }
    }
}