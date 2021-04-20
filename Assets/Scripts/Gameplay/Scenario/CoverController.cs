using System;
using System.Collections.Generic;
using Gameplay.Lighting;
using JetBrains.Annotations;
using UnityEngine;
using Utilities;

namespace Gameplay.Scenario
{
    public class CoverController : TriggerColliderController
    {
        [SerializeField] private GameObject _lights;

        private Collider[] _colliders;

        private Dictionary<int, LightEnum> _storedStateLightControllers = new Dictionary<int, LightEnum>();
        private Animator _animator;
        private bool _activated;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _colliders = GetComponents<Collider>();

            _activated = false;

            onTriggerEnter = HandlePlayerEnterArea;
            onTriggerExit = HandlePlayerLeaveArea;

            MapLightsState();
        }

        private void HandlePlayerEnterArea(Collider other)
        {
            if (!other.CompareTag(GameInternalTags.PLAYER)) return;
            Debug.Log("player enter the area");
            if(!_activated)
            {
                _animator.Play("FadeIn");
                EnableLights();
            }
            
            _activated = true;
        }

        private void HandlePlayerLeaveArea(Collider other)
        {
            if (!other.CompareTag(GameInternalTags.PLAYER)) return;
            Debug.Log("player left the area");
            if(!IsPositionInsideColliderArea(other.gameObject.transform.position))
            {
                _animator.Play("FadeOut");
                _activated = false;
            }
        }

        private bool IsPositionInsideColliderArea(Vector3 p_position)
        {
            foreach(Collider __collider in _colliders)
            {
                if (__collider.bounds.Contains(p_position)) return true;
            }

            return false;
        }

        [UsedImplicitly]
        private void DisableLights()
        {
            MapLightsState();
            _lights.SetActive(false);
        }

        private void EnableLights()
        {
            _lights.SetActive(true);
            SetLightsState();
        }

        private void MapLightsState()
        {
            _storedStateLightControllers.Clear();
            LightController[] __lights = _lights.gameObject.GetComponentsInChildren<LightController>();
            foreach (LightController light in __lights)
                _storedStateLightControllers.Add(light.GetHashCode(), light.lightState);
        }

        private void SetLightsState()
        {
            LightController[] lights = _lights.gameObject.GetComponentsInChildren<LightController>();
            foreach (LightController light in lights)
                light.SetLightState(_storedStateLightControllers[light.GetHashCode()]);
        }
    }
}