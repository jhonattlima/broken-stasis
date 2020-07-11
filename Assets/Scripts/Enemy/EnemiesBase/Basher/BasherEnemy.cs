using UnityEngine;

namespace Enemy
{
    public class BasherEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private BasherContainer _basherContainer;

        private EnemyStatesManager _stateManager;

        private EnemyAnimator _enemyAnimator;

        private BasherAI _basherAI;

        public void InitializeEnemy()
        {
            RegisterObjectsGraph();

            _basherAI.InitializeEnemy();
        }

        private void RegisterObjectsGraph()
        {
            _stateManager = new EnemyStatesManager();

            _enemyAnimator = new EnemyAnimator(
                _stateManager,
                _basherContainer.Animator
            );

            _basherAI = new BasherAI(
                _stateManager,
                new PatrolEnemy(
                    _stateManager,
                    _basherContainer.NavigationAgent,
                    _basherContainer.PatrolPointsGameObject,
                    _basherContainer.IdleTime,
                    _basherContainer.PatrolSpeedMultiplier
                ),
                new FollowEnemy(
                    _stateManager,
                    _basherContainer.NavigationAgent,
                    _basherContainer.InvestigateSpeedMultiplier,
                    _basherContainer.SprintSpeedMultiplier
                ),
                new AttackMeleeEnemy(
                    _stateManager,
                    _basherContainer.WeaponSensor,
                    _basherContainer.OriginPosition,
                    _basherContainer.AttackRange,
                    _basherContainer.Damage
                ),
                _basherContainer.NoiseSensor,
                _basherContainer.VisionSensor,
                _basherContainer.EnemyAnimationEventHandler
            );
        }

        public void RunUpdate()
        {
            _basherAI.RunUpdate();
        }
    }
}
