using Enemy.EnemyState;
using UnityEngine;

namespace Enemy.EnemiesBase
{
    public class EnemyAnimator
    {
        private const string IDLE = "Idle";
        private const string WALKING = "Walking";
        private const string RUNNING = "Running";
        private const string ATTACKING = "Attacking";

        private EnemyStateManager _stateManager;
        private Animator _animator;

        public EnemyAnimator(EnemyStateManager p_stateManager, Animator p_animator)
        {
            _stateManager = p_stateManager;
            _animator = p_animator;

            _stateManager.onStateChanged += HandleStateChanged;
        }

        private void HandleStateChanged(EnemyStateEnum p_enemyState)
        {
            ResetAllTriggers();

            switch (p_enemyState)
            {
                case EnemyStateEnum.IDLE:
                    _animator.SetTrigger(IDLE);
                    break;
                case EnemyStateEnum.INVESTIGATING:
                case EnemyStateEnum.PATROLLING:
                    _animator.SetTrigger(WALKING);
                    break;
                case EnemyStateEnum.RUNNING:
                    _animator.SetTrigger(RUNNING);
                    break;
                case EnemyStateEnum.ATTACKING:
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