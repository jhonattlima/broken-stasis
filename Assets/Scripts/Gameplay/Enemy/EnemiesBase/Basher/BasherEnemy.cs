using System;
using GameManagers;
using Gameplay.Enemy.Behaviours;
using Gameplay.Enemy.EnemyState;
using UnityEngine;

namespace Gameplay.Enemy.EnemiesBase
{
    public class BasherEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private BasherContainer _basherContainer;

        private EnemyStateManager _stateManager;
        private EnemyAnimator _enemyAnimator;
        private IEnemyAI _basherAI;
        private Action<int> _onPlayerDamaged;

        private void Awake()
        {
            if (_basherContainer == null)
                throw new MissingComponentException("BasherContainer not found in BasherEnemy!");
        }

        public void InitializeEnemy(Action<int> p_onPlayerDamaged)
        {
            _onPlayerDamaged = p_onPlayerDamaged;

            RegisterObjectsGraph();

            _basherAI.InitializeEnemy();

            GameStateManager.onStateChanged += HandleGameStateChanged;
        }

        private void RegisterObjectsGraph()
        {
            _stateManager = new EnemyStateManager();

            _enemyAnimator = new EnemyAnimator(
                _stateManager,
                _basherContainer.animator
            );

            _basherAI = new BasherAI(
                _stateManager,
                new PatrolEnemy(
                    _stateManager,
                    _basherContainer.navigationAgent,
                    _basherContainer.patrolPointsGameObject,
                    _basherContainer.idleTime,
                    _basherContainer.patrolSpeedMultiplier
                ),
                new FollowEnemy(
                    _stateManager,
                    _basherContainer.navigationAgent,
                    _basherContainer.investigateSpeedMultiplier,
                    _basherContainer.sprintSpeedMultiplier
                ),
                new AttackMeleeEnemy(
                    _stateManager,
                    _basherContainer.weaponSensor,
                    _basherContainer.originPosition,
                    _basherContainer.attackRange,
                    _basherContainer.damage,
                    _onPlayerDamaged
                ),
                _basherContainer.noiseSensor,
                _basherContainer.visionSensor,
                _basherContainer.enemyAnimationEventHandler,
                gameObject.transform.position
            );

            if (_basherContainer.basherType.Equals(BasherTypeEnum.STASIS))
            {
                _basherAI = new BasherStasisAI(
                    _basherAI,
                    _basherContainer.stasisSensor
                );
            }
        }

        public void RunUpdate()
        {
            _basherAI.RunUpdate();
        }

        private void HandleGameStateChanged(GameState p_gameState)
        {
            switch (p_gameState)
            {
                case GameState.GAMEOVER:
                    _basherAI.ResetEnemyAI();
                    break;
                default:
                    break;
            }
        }
    }
}
