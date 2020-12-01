using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class PatrolEnemy : IPatrolEnemy
    {
        private readonly EnemyStatesManager _stateManager;
        private readonly NavMeshAgent _navigationAgent;
        private readonly GameObject _patrolPointsGameObject;
        private readonly float _idleTime;
        private readonly float _patrolSpeedMultiplier;

        private List<Transform> _patrolPointsList;
        private int _patrolIndex;
        private bool _settingDestination;

        private float _initialSpeed;
        private float _initialAcceleration;
        private float _initialAngularSpeed;

        public PatrolEnemy(
            EnemyStatesManager p_stateManager,
            NavMeshAgent p_navigationAgent,
            GameObject p_patrolPointsGameObject,
            float p_idleTime,
            float p_patrolSpeedMultiplier)
        {
            _stateManager = p_stateManager;
            _navigationAgent = p_navigationAgent;
            _patrolPointsGameObject = p_patrolPointsGameObject;
            _idleTime = p_idleTime;
            _patrolSpeedMultiplier = p_patrolSpeedMultiplier;
        }

        public void InitializePatrolBehaviour()
        {
            _patrolIndex = 0;
            _settingDestination = false;
            _patrolPointsList = _patrolPointsGameObject.GetComponentsInChildren<Transform>().ToList();
            _patrolPointsList.Remove(_patrolPointsGameObject.transform);

            _stateManager.onStateChanged += HandleStateChanged;

            _initialSpeed = _navigationAgent.speed;
            _initialAcceleration = _navigationAgent.acceleration;
            _initialAngularSpeed = _navigationAgent.angularSpeed;

            PatrolToNextPoint();
        }

        private void HandleStateChanged(EnemyState p_enemyState)
        {
            switch (p_enemyState)
            {
                case EnemyState.IDLE:
                    ResetSpeed();
                    break;
                case EnemyState.PATROLLING:
                    SetPatrolDestination();
                    break;
                case EnemyState.INVESTIGATING:
                case EnemyState.RUNNING:
                    StopPatrolling();
                    break;
                default:
                    break;
            }
        }

        public void RunEnemyPatrol()
        {
            if (!IsEnemyPatrolling() || _navigationAgent.remainingDistance < 0.05f)
                if (!_settingDestination)
                    PatrolToNextPoint();
        }

        private bool IsEnemyPatrolling()
        {
            return (_stateManager.currentState == EnemyState.PATROLLING || _stateManager.currentState == EnemyState.IDLE);
        }

        private void PatrolToNextPoint()
        {
            _settingDestination = true;

            _stateManager.SetEnemyState(EnemyState.IDLE);

            TFWToolKit.Timer(_idleTime, delegate () 
            {
                _stateManager.SetEnemyState(EnemyState.PATROLLING);
                
                _navigationAgent.isStopped = false;

                _settingDestination = false;
            });

        }

        private void StopPatrolling()
        {
            _settingDestination = false;
            PatrolToNextPoint();
        }

        private void SetPatrolDestination()
        {
            _navigationAgent.SetDestination(_patrolPointsList[_patrolIndex].transform.position);

            if (_patrolIndex == _patrolPointsList.Count - 1)
                _patrolIndex = 0;
            else
                _patrolIndex++;
        }

        private void ResetSpeed()
        {
            _navigationAgent.speed = _initialSpeed * _patrolSpeedMultiplier;
            _navigationAgent.acceleration = _initialAcceleration * _patrolSpeedMultiplier;
            _navigationAgent.angularSpeed = _initialAngularSpeed * _patrolSpeedMultiplier;
        }
    }
}
