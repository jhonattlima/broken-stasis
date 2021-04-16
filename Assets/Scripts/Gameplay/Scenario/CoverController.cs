using System;
using System.Collections.Generic;
using Gameplay.Lighting;
using UnityEngine;
using Utilities;

namespace Gameplay.Scenario
{
    public class CoverController : TriggerColliderController
    {
        [SerializeField] private GameObject _lights;

        private Dictionary<int, LightEnum> _storedStateLightControllers = new Dictionary<int, LightEnum>();
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();

            onTriggerEnter = HandlePlayerEnterArea;
            onTriggerExit = HandlePlayerLeaveArea;
            MapLightsState();
        }

        private void HandlePlayerEnterArea(Collider other)
        {
            if (!other.CompareTag(GameInternalTags.PLAYER)) return;
            _animator.Play("FadeIn");
            EnableLights();
        }

        private void HandlePlayerLeaveArea(Collider other)
        {
            if (!other.CompareTag(GameInternalTags.PLAYER)) return;
            _animator.Play("FadeOut");
        }

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