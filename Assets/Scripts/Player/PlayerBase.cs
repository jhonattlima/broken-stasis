﻿using System;
using UnityEngine;

namespace Player
{
    public class PlayerBase : IFixedUpdateBehaviour
    {
        #region PUBLIC FIELDS
        public Action<int> onPlayerDamaged;

        //TODO 23/07/2020 -> Implement player health gain
        public Action<int> onPlayerHealthIncrease;
        public Action<PlayerSuitEnum> onSuitChange;
        #endregion

        #region PRIVATE FIELDS
        private PlayerContainer _playerContainer;
        private PlayerHealth _playerHealth;
        private PlayerMovement _playerMovement;
        private PlayerSoundColliderActivator _playerSoundColliderActivator;
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

            RegisterPlayerAnimator();

            _playerHealth = new PlayerHealth();
        }

        private void RegisterPlayerAnimator()
        {
            foreach(PlayerSuitData __playerSuit in _playerContainer.suits)
            {
                PlayerAnimator __playerAnimator = new PlayerAnimator(__playerSuit.suitAnimator, __playerSuit.suitAnimationEventHandler);
            }
        }

        public void RunFixedUpdate()
        {
            _playerMovement.RunFixedUpdate();
        }

        private void HandleSuitChange(PlayerSuitEnum p_playerSuit)
        {
            foreach(PlayerSuitData __playerSuit in _playerContainer.suits)
            {
                __playerSuit.suitGameObject.SetActive(__playerSuit.suitType == p_playerSuit);
            }
        }
    }
}
