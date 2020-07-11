using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class BasherContainer : MonoBehaviour
    {
        public NavMeshAgent NavigationAgent;

        [Header("Animator References")]
        [Space(5)]
        public Animator Animator;
        public EnemyAnimationEventHandler EnemyAnimationEventHandler;
        
        [Header("Patrol Variables")]
        [Space(5)]
        public GameObject PatrolPointsGameObject;
        public float IdleTime = 0.75f;
        public float PatrolSpeedMultiplier = 0.5f;
        
        [Header("Follow Variables")]
        [Space(5)]
        public float InvestigateSpeedMultiplier = 0.75f;
        public float SprintSpeedMultiplier = 1f;
        
        [Header("Attack Variables")]
        [Space(5)]
        public SensorDamagePlayer WeaponSensor;
        public Transform OriginPosition;
        public float AttackRange = 1.75f;
        public int Damage = 1;

        [Header("Sensors")]
        [Space(5)]
        public SensorNoise NoiseSensor;
        public SensorVision VisionSensor;
    }
}
