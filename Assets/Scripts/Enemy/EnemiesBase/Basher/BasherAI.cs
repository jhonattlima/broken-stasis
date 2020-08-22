
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class BasherAI : IEnemyAI
    {
        private readonly EnemyStatesManager _stateManager;
        private readonly IPatrolEnemy _patrolBehaviour;
        private readonly IFollowEnemy _followBehaviour;
        private readonly IAttackMeleeEnemy _attackMeleeBehaviour;
        private readonly SensorNoise _noiseSensor;
        private readonly SensorVision _visionSensor;
        private readonly EnemyAnimationEventHandler _enemyAnimationEventHandler;
        
        private bool _isHearingPlayer;
        private bool _isViewingPlayer;

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
            if (CanEnterPatrolState())
                _patrolBehaviour.RunEnemyPatrol();
            else if (_isViewingPlayer)
                _attackMeleeBehaviour.RunUpdate();
            else if (_stateManager.currentState == EnemyState.INVESTIGATING 
                    || _stateManager.currentState == EnemyState.RUNNING) {
                _followBehaviour.RunEnemyFollow();
            }
        }

        public bool CanEnterPatrolState() {
            return !_isHearingPlayer 
            && !_isViewingPlayer
            && _stateManager.currentState != EnemyState.ATTACKING
            && _stateManager.currentState != EnemyState.RUNNING
            && _stateManager.currentState != EnemyState.INVESTIGATING;
        }

        private void HandlePlayerEnteredSoundSensor(Transform p_playerLastKnownPosition)
        {
            HandleHearingPlayer(p_playerLastKnownPosition);
        }

        private void HandlePlayerRemainsInSoundSensor(Transform p_playerLastKnownPosition)
        {
            HandleHearingPlayer(p_playerLastKnownPosition);
        }

        private void HandlePlayerLeftSoundSensor(Transform p_playerLastKnownPosition)
        {
            _isHearingPlayer = false;
            if(_stateManager.currentState != EnemyState.ATTACKING || _stateManager.currentState != EnemyState.RUNNING)
                _followBehaviour.InvestigatePosition(p_playerLastKnownPosition);  
        }

        private void HandleHearingPlayer(Transform p_playerLastKnownPosition)
        {
            _isHearingPlayer = true;
            if (_stateManager.currentState == EnemyState.ATTACKING)
            {
                _enemyAnimationEventHandler.OnAttackAnimationEnd = delegate ()
                {
                    if (!_attackMeleeBehaviour.CanAttack(p_playerLastKnownPosition.position) 
                        && !_isViewingPlayer)
                        _followBehaviour.InvestigatePosition(p_playerLastKnownPosition);
                };
            }
            else if (_stateManager.currentState != EnemyState.RUNNING
                    && !_isViewingPlayer)
                _followBehaviour.InvestigatePosition(p_playerLastKnownPosition);
        }

        private void HandlePlayerEnteredVisionSensor(Transform p_playerLastKnownPosition)
        {
            _isViewingPlayer = true;

            if (_attackMeleeBehaviour.CanAttack(p_playerLastKnownPosition.position))
                _stateManager.SetEnemyState(EnemyState.ATTACKING);
            else
                _followBehaviour.SprintToPosition(p_playerLastKnownPosition);
        }

        private void HandlePlayerRemainsInVisionSensor(Transform p_playerLastKnownPosition)
        {
            _isViewingPlayer = true;

            if (_stateManager.currentState == EnemyState.ATTACKING)
            {
                _enemyAnimationEventHandler.OnAttackAnimationEnd = delegate ()
                {
                    if (!_attackMeleeBehaviour.CanAttack(p_playerLastKnownPosition.position))
                        _followBehaviour.SprintToPosition(p_playerLastKnownPosition);
                };
            }
            else if (_attackMeleeBehaviour.CanAttack(p_playerLastKnownPosition.position))
                _stateManager.SetEnemyState(EnemyState.ATTACKING);
            else
                _followBehaviour.SprintToPosition(p_playerLastKnownPosition);
        }

        private void HandlePlayerLeftVisionSensor(Transform p_playerLastKnownPosition)
        {
            _isViewingPlayer = false;
            _followBehaviour.SprintToPosition(p_playerLastKnownPosition);
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
