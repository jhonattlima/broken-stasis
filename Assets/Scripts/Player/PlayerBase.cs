namespace Player
{
    public class PlayerBase : IFixedUpdateBehaviour
    {
        #region PRIVATE FIELDS
        private PlayerContainer _playerContainer;
        private PlayerMovement _playerMovement;
        private PlayerAnimator _playerAnimator;
        private PlayerSoundColliderActivator _playerSoundColliderActivator;
        #endregion

        public PlayerBase (PlayerContainer p_playerContainer)
        {
            _playerContainer = p_playerContainer;
        }

        public void InitializePlayer()
        {
            PlayerStatesManager.onStateChanged = null;
            PlayerStatesManager.SetPlayerState(PlayerState.STATIC);
            
            _playerMovement = new PlayerMovement( _playerContainer.characterController,
                _playerContainer.playerTransform
            );
            
            _playerSoundColliderActivator = new PlayerSoundColliderActivator ( _playerContainer.lowSoundCollider,
                _playerContainer.mediumSoundCollider,
                _playerContainer.loudSoundCollider
            );    

            _playerAnimator = new PlayerAnimator (_playerContainer.playerAnimator);                                      
        }

        public void RunFixedUpdate()
        {
            _playerMovement.RunFixedUpdate();
        }
    }
}
