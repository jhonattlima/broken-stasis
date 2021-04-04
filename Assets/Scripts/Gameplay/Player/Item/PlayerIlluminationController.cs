using System;
using UnityEngine;

namespace Gameplay.Player.Item
{
    [Serializable]
    public struct IlluminationState
    {
        public bool enabled;
        public bool toggledOn;
    }

    public class PlayerIlluminationController
    {
        private readonly GameObject _illuminationObject;
        private IlluminationState _illuminationState;

        public PlayerIlluminationController(GameObject p_illumination)
        {
            _illuminationObject = p_illumination;
            _illuminationObject.SetActive(false);
        }

        public IlluminationState lanternState
        {
            get 
            { 
                return _illuminationState; 
            }
            
            set 
            {
                _illuminationState = value;

                _illuminationObject.SetActive(_illuminationState.toggledOn);
            }
        }

        public void SetActive(bool p_active)
        {
            _illuminationState.enabled = p_active;
        }

        public void Toggle()
        {
            if(_illuminationState.enabled)
            {
                _illuminationState.toggledOn = !_illuminationState.toggledOn;
                _illuminationObject.SetActive(_illuminationState.toggledOn);
            }
        }
    }
}