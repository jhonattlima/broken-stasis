using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class FollowEnemy : IFollowEnemy
    {
        private readonly EnemyStatesManager _stateManager;
        private readonly NavMeshAgent _navMeshAgent;
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
            _navMeshAgent = p_navigationAgent;
            _investigateSpeedMultiplier = p_investigateSpeedMultiplier;
            _sprintSpeedMultiplier = p_sprintSpeedMultiplier;

            _initialSpeed = _navMeshAgent.speed;
            _initialAcceleration = _navMeshAgent.acceleration;
            _initialAngularSpeed = _navMeshAgent.angularSpeed;

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
                    _navMeshAgent.speed = _initialSpeed * _investigateSpeedMultiplier;
                    _navMeshAgent.acceleration = _initialAcceleration * _investigateSpeedMultiplier;
                    _navMeshAgent.angularSpeed = _initialAngularSpeed * _investigateSpeedMultiplier;
                    break;
                case EnemyState.RUNNING:
                    _navMeshAgent.speed = _initialSpeed * _sprintSpeedMultiplier;
                    _navMeshAgent.acceleration = _initialAcceleration * _sprintSpeedMultiplier;
                    _navMeshAgent.angularSpeed = _initialAngularSpeed * _sprintSpeedMultiplier;
                    break;
                case EnemyState.ATTACKING:
                    StopNavigation();
                    break;
                default:
                    break;
            }
        }

        public void RunEnemyFollow() {
            if((_navMeshAgent.remainingDistance < 0.2f))
                _stateManager.SetEnemyState(EnemyState.IDLE);
        }

        public void InvestigatePosition(Transform p_destinationPosition)
        {
            _stateManager.SetEnemyState(EnemyState.INVESTIGATING);
            SetNavigationDestination(p_destinationPosition);
        }

        public void SprintToPosition(Transform p_destinationPosition)
        {
            _stateManager.SetEnemyState(EnemyState.RUNNING);
            SetNavigationDestination(p_destinationPosition);
        }

        private void SetNavigationDestination(Transform p_destinationPosition)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(p_destinationPosition.position);
        }

        private void StopNavigation()
        {
            _navMeshAgent.isStopped = true;
        }
    }
}