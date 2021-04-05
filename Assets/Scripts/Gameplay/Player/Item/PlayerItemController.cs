using System;
using SaveSystem.Player;
using Utilities;

namespace Gameplay.Player.Item
{
    public class PlayerItemController : IUpdateBehaviour
    {
        public Action<bool> onActivatePlayerIllumination;
        private readonly PlayerIlluminationController _playerIllumination;

        public PlayerItemController(PlayerIlluminationController p_playerIllumination)
        {
            _playerIllumination = p_playerIllumination;

            onActivatePlayerIllumination = HandlePlayerIllumination;
        }

        public void RunUpdate()
        {
            if(InputController.GamePlay.ToggleIllumination())
            {
                _playerIllumination.Toggle();
            }
        }

        private void HandlePlayerIllumination(bool p_active)
        {
            _playerIllumination.SetActive(p_active);
        }

        public PlayerIlluminationSaveData GetIlluminationState()
        {
            return _playerIllumination.lanternState;
        }

        public void SetIlluminationState(PlayerIlluminationSaveData p_illuminationState)
        {
            _playerIllumination.lanternState = p_illuminationState;
        }
    }
}