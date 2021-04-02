using System;
using Gameplay.Player.Animation;
using Gameplay.Player.Health;
using Gameplay.Player.Item;
using Gameplay.Player.Motion;
using Gameplay.Player.Sensors;
using SaveSystem;
using Utilities;

namespace Gameplay.Player
{
    public class PlayerBase : IFixedUpdateBehaviour, IUpdateBehaviour
    {
        #region PUBLIC FIELDS
        public Action<int> onPlayerDamaged;

        //TODO 23/07/2020 -> Implement player health gain
        public Action<int> onPlayerHealthIncrease;
        public Action<PlayerSuitEnum> onSuitChange;
        public Action<bool> onActivateIllumination;
        #endregion

        #region PRIVATE FIELDS
        private PlayerContainer _playerContainer;
        private PlayerHealth _playerHealth;
        private PlayerMovement _playerMovement;
        private PlayerSoundColliderActivator _playerSoundColliderActivator;
        private PlayerItemController _playerItemController;
        #endregion

        public PlayerBase(PlayerContainer p_playerContainer)
        {
            _playerContainer = p_playerContainer;
        }

        public void InitializePlayer()
        {
            PlayerStatesManager.onStateChanged = null;
            PlayerStatesManager.SetPlayerState(PlayerState.STATIC);

            RegisterObjectsGraph();

            onPlayerDamaged += _playerHealth.ReceiveDamage;
            onPlayerHealthIncrease += _playerHealth.IncreaseHealth;
            onSuitChange = HandleSuitChange;
            onActivateIllumination = _playerItemController.onActivatePlayerIllumination;
        }

        private void RegisterObjectsGraph()
        {
            _playerMovement = new PlayerMovement(
                _playerContainer.characterController,
                _playerContainer.playerTransform
            );

            _playerSoundColliderActivator = new PlayerSoundColliderActivator(
                _playerContainer.lowSoundCollider,
                _playerContainer.mediumSoundCollider,
                _playerContainer.loudSoundCollider
            );

            _playerItemController = new PlayerItemController(
                new PlayerIlluminationController(
                    _playerContainer.playerIlluminationGameObject
                )
            );

            RegisterPlayerAnimator();

            _playerHealth = new PlayerHealth();
        }

        private void RegisterPlayerAnimator()
        {
            foreach (PlayerSuitData __playerSuit in _playerContainer.suits)
            {
                PlayerAnimator __playerAnimator = new PlayerAnimator(__playerSuit.suitAnimator, __playerSuit.suitAnimationEventHandler);
            }
        }
        
        public void RunUpdate()
        {
            _playerItemController.RunUpdate();
        }

        public void RunFixedUpdate()
        {
            _playerMovement.RunFixedUpdate();
        }

        // TODO: Transferir para PlayerItemController
        private void HandleSuitChange(PlayerSuitEnum p_playerSuit)
        {
            foreach (PlayerSuitData __playerSuit in _playerContainer.suits)
            {
                __playerSuit.suitGameObject.SetActive(__playerSuit.suitType == p_playerSuit);
            }
        }

        // TODO: Transferir para PlayerItemController
        private PlayerSuitEnum GetActiveSuit()
        {
            foreach (PlayerSuitData __playerSuit in _playerContainer.suits)
            {
                if (__playerSuit.suitGameObject.active)
                {
                    return __playerSuit.suitType;
                }
            }

            return PlayerSuitEnum.NAKED;
        }

        public GameSaveData GetPlayerSaveData()
        {
            GameSaveData __gameSaveData = new GameSaveData();

            __gameSaveData.playerSuit = GetActiveSuit();
            __gameSaveData.playerPosition = _playerContainer.playerTransform.position;
            __gameSaveData.playerHealth = _playerHealth.GetPlayerHealth();
            __gameSaveData.playerIlluminationState = _playerItemController.GetIlluminationState();

            return __gameSaveData;
        }

        public void SetPlayerSaveData(GameSaveData p_gameSaveData)
        {
            _playerContainer.playerTransform.position = p_gameSaveData.playerPosition;
            HandleSuitChange(p_gameSaveData.playerSuit);
            _playerHealth.SetPlayerHealth(p_gameSaveData.playerHealth);
            _playerItemController.SetIlluminationState(p_gameSaveData.playerIlluminationState);
        }
    }
}
