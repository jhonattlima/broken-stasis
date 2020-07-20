﻿using System;
using UnityEngine;

namespace Enemy
{
    public class SensorVision : MonoBehaviour
    {
        [SerializeField] private Transform _eyesTransform;

        public Action<Transform> onPlayerDetected;
        public Action<Transform> onPlayerRemainsDetected;
        public Action<Transform> onPlayerLeftDetection;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameInternalTags.PLAYER) && HasDirectViewOfPlayer(other.gameObject))
                if (onPlayerDetected != null) onPlayerDetected(other.transform);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag(GameInternalTags.PLAYER) && HasDirectViewOfPlayer(other.gameObject))
                if (onPlayerRemainsDetected != null) onPlayerRemainsDetected(other.transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag(GameInternalTags.PLAYER))
                if (onPlayerLeftDetection != null) onPlayerLeftDetection(other.transform);
        }

        private bool HasDirectViewOfPlayer(GameObject p_playerGameObject)
        {
            RaycastHit __hit;
            Vector3 __fromPosition = _eyesTransform.position;
            Vector3 __toPosition = p_playerGameObject.transform.position;
            Vector3 __direction = __toPosition - __fromPosition;

            Debug.DrawRay(__fromPosition, __direction, Color.green);
        
            if(Physics.Raycast(__fromPosition,__direction,out __hit, 50f))
                return __hit.collider.CompareTag(GameInternalTags.PLAYER);

            return false;
        }
    }
}
