﻿using UnityEngine;
using VariableManagement;

namespace CameraScripts
{
    public class CameraFollowPlayer : IFixedUpdateBehaviour
    {
        private readonly Transform _playerTransform;
        private readonly Transform _cameraTransform;

        public CameraFollowPlayer(Transform p_playerTransform, Transform p_cameraTransform)
        {
            _playerTransform = p_playerTransform;
            _cameraTransform = p_cameraTransform;
        }

        private Vector3 _mousePosInWorld
        {
            get
            {
                Ray __ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit __hit;
                Physics.Raycast(__ray, out __hit, 50f);
                return __hit.point;
            }
        }

        public void RunFixedUpdate()
        {
            float __distToMove = Vector3.Distance(_mousePosInWorld, _playerTransform.position) * VariablesManager.cameraVariables.cameraDistanceFromPlayer;
            Vector3 __distanceVectorNormalized = (_mousePosInWorld - _playerTransform.position).normalized;
            Vector3 __targetPosition = _playerTransform.position + (__distanceVectorNormalized * __distToMove);
            _cameraTransform.position = new Vector3(__targetPosition.x, _cameraTransform.position.y, __targetPosition.z);
        }
    }
}