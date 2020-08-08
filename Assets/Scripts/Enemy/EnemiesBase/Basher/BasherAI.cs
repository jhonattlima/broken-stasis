
using Player;
using UnityEngine;

namespace Enemy
{
    public class BasherAI : IEnemyAI
    {
        private bool _isHearingPlayer;
        private bool _isViewingPlayer;

        private readonly EnemyStatesManager _stateManager;

        private readonly IPatrolEnemy _patrolBehaviour;
        private readonly IFollowEnemy _followBehaviour;
        private readonly IAttackMeleeEnemy _attackMeleeBehaviour;

        private readonly SensorNoise _noiseSensor;
        private readonly SensorVision _visionSensor;

        private readonly EnemyAnimationEventHandler _enemyAnimationEventHandler;

        public BasherAI(EnemyStatesManager p_stateManager,
            IPatrolEnemy p_patrolBehaviour,
            IFollowEnemy p_followBehaviour,
            IAttackMeleeEnemy p_attackMeleeBehaviour,
            SensorNoise p_noiseSensor,
            SensorVision p_visionSensor,
            EnemyAnimationEventHandler p_enemyAnimationEventHandler)
        {
            _stateManager = p_stateManager;
            _patrolBehaviour = p_patrolBehaviour;
            _followBehaviour = p_followBehaviour;
            _attackMeleeBehaviour = p_attackMeleeBehaviour;
            _noiseSensor = p_noiseSensor;
            _visionSensor = p_visionSensor;
            _enemyAnimationEventHandler = p_enemyAnimationEventHandler;
        }

        public void InitializeEnemy()
        {
            ResetEnemyAI();

            _noiseSensor.onPlayerDetected += HandlePlayerEnteredSoundSensor;
            _noiseSensor.onPlayerRemainsDetected += HandlePlayerRemainsInSoundSensor;
            _noiseSensor.onPlayerLeftDetection += HandlePlayerLeftSoundSensor;

            _visionSensor.onPlayerDetected += HandlePlayerEnteredVisionSensor;
            _visionSensor.onPlayerRemainsDetected += HandlePlayerRemainsInVisionSensor;
            _visionSensor.onPlayerLeftDetection += HandlePlayerLeftVisionSensor;
        }

        public void RunUpdate()
        {
            if (!_isHearingPlayer && !_isViewingPlayer)
                _patrolBehaviour.RunEnemyPatrol();
            else if (_isViewingPlayer)
                _attackMeleeBehaviour.RunUpdate();
        }

        private void HandlePlayerEnteredSoundSensor(Transform p_playerPosition)
        {
            _isHearingPlayer = true;

            HandleHearingPlayer(p_playerPosition);
        }

        private void HandlePlayerRemainsInSoundSensor(Transform p_playerPosition)
        {
            HandleHearingPlayer(p_playerPosition);
        }

        private void HandleHearingPlayer(Transform p_playerPosition)
        {
            if (_stateManager.currentState == EnemyState.ATTACKING)
            {
                _enemyAnimationEventHandler.OnAttackAnimationEnd = delegate ()
                {
                    if (!_attackMeleeBehaviour.CanAttack(p_playerPosition.position) || !_isViewingPlayer)
                        _followBehaviour.InvestigatePosition(p_playerPosition);
                };
            }
            else if (!_isViewingPlayer)
                _followBehaviour.InvestigatePosition(p_playerPosition);
        }

        private void HandlePlayerLeftSoundSensor(Transform p_playerPosition)
        {
            _isHearingPlayer = false;
        }

        private void HandlePlayerEnteredVisionSensor(Transform p_playerPosition)
        {
            _isViewingPlayer = true;

            if (_attackMeleeBehaviour.CanAttack(p_playerPosition.position))
                _stateManager.SetEnemyState(EnemyState.ATTACKING);
            else
                _followBehaviour.SprintToPosition(p_playerPosition);
        }

        private void HandlePlayerRemainsInVisionSensor(Transform p_playerPosition)
        {
            _isViewingPlayer = true;

            if (_stateManager.currentState == EnemyState.ATTACKING)
            {
                _enemyAnimationEventHandler.OnAttackAnimationEnd = delegate ()
                {
                    if (!_attackMeleeBehaviour.CanAttack(p_playerPosition.position))
                        _followBehaviour.SprintToPosition(p_playerPosition);
                };
            }
            else if (_attackMeleeBehaviour.CanAttack(p_playerPosition.position))
                _stateManager.SetEnemyState(EnemyState.ATTACKING);
            else
                _followBehaviour.SprintToPosition(p_playerPosition);
        }

        private void HandlePlayerLeftVisionSensor(Transform p_playerPosition)
        {
            _isViewingPlayer = false;
        }

        public void ResetEnemyAI()
        {
            _isHearingPlayer = false;
            _isViewingPlayer = false;

            _noiseSensor.onPlayerDetected = null;
            _noiseSensor.onPlayerRemainsDetected = null;
            _noiseSensor.onPlayerLeftDetection = null;

            _visionSensor.onPlayerDetected = null;
            _visionSensor.onPlayerRemainsDetected = null;
            _visionSensor.onPlayerLeftDetection = null;
        }
    }
}
