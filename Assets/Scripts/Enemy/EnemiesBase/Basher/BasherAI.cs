
using Audio;
using UnityEngine;

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

        private Vector3 _basherPosition;
        private AudioSource _idleSound;

        public BasherAI(EnemyStatesManager p_stateManager,
            IPatrolEnemy p_patrolBehaviour,
            IFollowEnemy p_followBehaviour,
            IAttackMeleeEnemy p_attackMeleeBehaviour,
            SensorNoise p_noiseSensor,
            SensorVision p_visionSensor,
            EnemyAnimationEventHandler p_enemyAnimationEventHandler,
            Vector3 p_basherPosition)
        {
            _stateManager = p_stateManager;
            _patrolBehaviour = p_patrolBehaviour;
            _followBehaviour = p_followBehaviour;
            _attackMeleeBehaviour = p_attackMeleeBehaviour;
            _noiseSensor = p_noiseSensor;
            _visionSensor = p_visionSensor;
            _enemyAnimationEventHandler = p_enemyAnimationEventHandler;
            _basherPosition = p_basherPosition;
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

            _enemyAnimationEventHandler.OnStep += delegate ()
            {
                AudioManager.instance.PlayAtPosition(AudioNameEnum.BASHER_STEP, _basherPosition);
            };
            _enemyAnimationEventHandler.OnAttack += delegate ()
            {
                AudioManager.instance.PlayAtPosition(AudioNameEnum.BASHER_ATTACK, _basherPosition);
            };

            _stateManager.onStateChanged += HandleStateChanged;

            _idleSound = AudioManager.instance.PlayAtPosition(AudioNameEnum.BASHER_IDLE, _basherPosition, true);
        }

        private void HandleStateChanged(EnemyState p_enemyState)
        {
            switch (p_enemyState)
            {
                case EnemyState.ATTACKING:
                    _idleSound.Pause();
                    break;
                default:
                    _idleSound.Play();
                    break;
            }
        }

        public void RunUpdate()
        {
            _followBehaviour.RunFollowEnemy();

            if (CanPatrol() || GameStateManager.currentState == GameState.GAMEOVER)
                _patrolBehaviour.RunEnemyPatrol();
            else if (_isViewingPlayer)
                _attackMeleeBehaviour.RunUpdate();
        }

        private bool CanPatrol()
        {
            return (_stateManager.currentState != EnemyState.INVESTIGATING 
                    && _stateManager.currentState != EnemyState.RUNNING
                    && _stateManager.currentState != EnemyState.ATTACKING);
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
