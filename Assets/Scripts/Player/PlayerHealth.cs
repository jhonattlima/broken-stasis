using UnityEngine;
using VariableManagement;

namespace Player
{
    public class PlayerHealth
    {
        private int _playerMaxHealth;
        private int _playerCurrentHealth;
        private readonly CharacterController _characterController;

        public PlayerHealth(CharacterController p_characterController)
        {
            _characterController = p_characterController;

            if(VariablesManager.playerVariables.maxHealth <= 0)
                throw new System.Exception("Player max health variable is " + VariablesManager.playerVariables.maxHealth + "Minimum of life is 1.");
            
            this._playerMaxHealth = VariablesManager.playerVariables.maxHealth;
            this._playerCurrentHealth = this._playerMaxHealth;
        }

        public void ReceiveDamage(int damage)
        {
            if (_playerCurrentHealth - damage < 0)
                _playerCurrentHealth = 0;
            else
                _playerCurrentHealth -= damage;

            if (_playerCurrentHealth == 0)
                HandlePlayerDeath();
            else
                PlayerStatesManager.SetPlayerState(PlayerState.HIT);
        }

        public void IncreaseHealth(int lifePoints)
        {

        }

        private void HandlePlayerDeath()
        {        
            PlayerStatesManager.SetPlayerState(PlayerState.DEAD);
            GameStateManager.SetGameState(GameState.GAMEOVER);
        }
    }
}
