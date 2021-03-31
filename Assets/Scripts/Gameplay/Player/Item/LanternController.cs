using UnityEngine;

namespace Gameplay.Player.Item
{
    public struct LanternState
    {
        public bool enabled;
        public bool toggledOn;
    }

    public class LanternController
    {
        private readonly GameObject _lantern;
        private LanternState _lanternState;
        public LanternState lanternState
        {
            get { return _lanternState; }
        }

        public void Enable()
        {
            _lanternState.enabled = true;
        }

        public void Disable()
        {
            _lanternState.enabled = false;
        }

        public void Toggle()
        {
            if(_lanternState.enabled)
            {
                _lanternState.toggledOn = !_lanternState.toggledOn;
                _lantern.SetActive(_lanternState.toggledOn);
            }
        }
    }
}