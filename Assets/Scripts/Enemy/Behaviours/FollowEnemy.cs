using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class FollowEnemy : IFollowEnemy
    {
        private readonly EnemyStatesManager _stateManager;
        private readonly NavMeshAgent _navigationAgent;
        private readonly float _investigateSpeedMultiplier;
        private readonly float _sprintSpeedMultiplier;

        private float _initialSpeed;
        private float _initialAcceleration;
        private float _initialAngularSpeed;

        public FollowEnemy(
            EnemyStatesManager p_stateManager,
            NavMeshAgent p_navigationAgent,
            float p_investigateSpeedMultiplier,
            float p_sprintSpeedMultiplier)
        {
            _stateManager = p_stateManager;
            _navigationAgent = p_navigationAgent;
            _investigateSpeedMultiplier = p_investigateSpeedMultiplier;
            _sprintSpeedMultiplier = p_sprintSpeedMultiplier;

            _initialSpeed = _navigationAgent.speed;
            _initialAcceleration = _navigationAgent.acceleration;
            _initialAngularSpeed = _navigationAgent.angularSpeed;

            InitializeFollowBehaviour();
        }

        private void InitializeFollowBehaviour()
        {
            _stateManager.onStateChanged += HandleStateChanged;
        }

        private void HandleStateChanged(EnemyState p_enemyState)
        {
            switch (p_enemyState)
            {
                case EnemyState.INVESTIGATING:
                    _navigationAgent.speed = _initialSpeed * _investigateSpeedMultiplier;
                    _navigationAgent.acceleration = _initialAcceleration * _investigateSpeedMultiplier;
                    _navigationAgent.angularSpeed = _initialAngularSpeed * _investigateSpeedMultiplier;
                    break;
                case EnemyState.RUNNING:
                    _navigationAgent.speed = _initialSpeed * _sprintSpeedMultiplier;
                    _navigationAgent.acceleration = _initialAcceleration * _sprintSpeedMultiplier;
                    _navigationAgent.angularSpeed = _initialAngularSpeed * _sprintSpeedMultiplier;
                    break;
                case EnemyState.ATTACKING:
                    StopNavigation();
                    break;
                default:
                    break;
            }
        }

        public void RunFollowEnemy()
        {
            if(IsEnemyFollowing() && _navigationAgent.remainingDistance < 0.05f)
                _stateManager.SetEnemyState(EnemyState.IDLE);
        }

        private bool IsEnemyFollowing()
        {
            return (_stateManager.currentState == EnemyState.RUNNING || _stateManager.currentState == EnemyState.INVESTIGATING);
        }

        public void InvestigatePosition(Transform p_destinationPosition)
        {
            SetNavigationDestination(p_destinationPosition);

            if (_stateManager.currentState != EnemyState.RUNNING)
                _stateManager.SetEnemyState(EnemyState.INVESTIGATING);
        }

        public void SprintToPosition(Transform p_destinationPosition)
        {
            SetNavigationDestination(p_destinationPosition);

            _stateManager.SetEnemyState(EnemyState.RUNNING);
        }

        private void StopNavigation()
        {
            _navigationAgent.isStopped = true;
        }

        private void SetNavigationDestination(Transform p_destinationPosition)
        {
            _navigationAgent.isStopped = false;

            _navigationAgent.SetDestination(p_destinationPosition.position);
        }        
    }
}