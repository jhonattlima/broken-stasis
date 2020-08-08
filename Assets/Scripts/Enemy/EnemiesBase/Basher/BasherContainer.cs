﻿using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class BasherContainer : MonoBehaviour
    {
        public NavMeshAgent navigationAgent;

        [Header("Animator References")]
        [Space(5)]
        public Animator animator;
        public EnemyAnimationEventHandler enemyAnimationEventHandler;

        [Header("Patrol Variables")]
        [Space(5)]
        public GameObject patrolPointsGameObject;
        public float idleTime = 0.75f;
        public float patrolSpeedMultiplier = 0.5f;

        [Header("Follow Variables")]
        [Space(5)]
        public float investigateSpeedMultiplier = 0.75f;
        public float sprintSpeedMultiplier = 1f;

        [Header("Attack Variables")]
        [Space(5)]
        public SensorDamagePlayer weaponSensor;
        public Transform originPosition;
        public float attackRange = 1.75f;
        public int damage = 1;

        [Header("Sensors")]
        [Space(5)]
        public SensorNoise noiseSensor;
        public SensorVision visionSensor;

        private void Awake()
        {
            if (navigationAgent == null)
                throw new MissingComponentException("NavigationAgent not found in BasherContainer!");
            if (animator == null)
                throw new MissingComponentException("Animator not found in BasherContainer!");
            if (enemyAnimationEventHandler == null)
                throw new MissingComponentException("EnemyAnimationEventHandler not found in BasherContainer!");
            if (visionSensor == null)
                throw new MissingComponentException("VisionSensor not found in BasherContainer!");
            if (noiseSensor == null)
                throw new MissingComponentException("NoiseSensor not found in BasherContainer!");
            if (originPosition == null)
                throw new MissingComponentException("OriginPosition not found in BasherContainer!");
            if (weaponSensor == null)
                throw new MissingComponentException("WeaponSensor not found in BasherContainer!");
            if (patrolPointsGameObject == null)
                throw new MissingComponentException("PatrolPointsGameObject not found in BasherContainer!");
        }
    }
}
