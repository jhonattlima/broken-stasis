using System;

namespace Player
{
    public class PlayerBase : IFixedUpdateBehaviour
    {
        #region PUBLIC FIELDS
        public Action<int> onPlayerDamaged;

        //TODO 23/07/2020 -> Implement player health gain
        public Action<int> onPlayerHealthIncrease;
        #endregion

        #region PRIVATE FIELDS
        private PlayerContainer _playerContainer;
        private PlayerHealth _playerHealth;
        private PlayerMovement _playerMovement;
        private PlayerAnimator _playerAnimator;
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

            _playerAnimator = new PlayerAnimator(_playerContainer.playerAnimator,
                                                    _playerContainer.playerAnimationEventHandler);

            _playerHealth = new PlayerHealth();
        }

        public void RunFixedUpdate()
        {
            _playerMovement.RunFixedUpdate();
        }
    }
}
