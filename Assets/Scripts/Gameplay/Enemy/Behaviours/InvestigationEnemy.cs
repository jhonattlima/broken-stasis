using Gameplay.Enemy.EnemyState;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.Enemy.Behaviours
{
    public class InvestigationEnemy
    {
        private readonly EnemyStateManager _stateManager;
        private readonly NavMeshAgent _navigationAgent;
        private readonly GameObject _patrolPointsGameObject;
        private readonly float _idleTime;
        private readonly float _patrolSpeedMultiplier;
    }
}