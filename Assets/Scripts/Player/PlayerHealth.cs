using Audio;
using UnityEngine;
using VariableManagement;

namespace Player
{
    public enum PlayerHealthState
    {
        DEAD = 0,
        CRITICAL = 1,
        DANGER = 2,
        FINE = 3
    }

    public class PlayerHealth
    {
        private int _playerMaxHealth;
        private int _playerCurrentHealth;
        private PlayerHealthState _playerHealthState;
        private AudioSource _heartBeatAudio;

        public PlayerHealth()
        {
            if(VariablesManager.playerVariables.maxHealth <= 0)
                throw new System.Exception("Player max health variable is " + VariablesManager.playerVariables.maxHealth + "Minimum of life is 1.");
            
            this._playerMaxHealth = VariablesManager.playerVariables.maxHealth;
            this._playerCurrentHealth = this._playerMaxHealth;

            _playerHealthState = (PlayerHealthState) _playerCurrentHealth;

            HandleHeartBeat();
        }

        public void ReceiveDamage(int p_damage)
        {
            if (_playerCurrentHealth - p_damage < 0)
                _playerCurrentHealth = 0;
            else
                _playerCurrentHealth -= p_damage;

            if (_playerCurrentHealth == 0)
                HandlePlayerDeath();
            else
            {
                AudioManager.instance.Play(AudioNameEnum.PLAYER_HIT);
                PlayerStatesManager.SetPlayerState(PlayerState.HIT);
            }
            
            _playerHealthState = (PlayerHealthState) _playerCurrentHealth;

            HandleHeartBeat();
        }

        public void IncreaseHealth(int p_lifePoints)
        {
            _playerCurrentHealth = p_lifePoints;
            
            if(_playerCurrentHealth > _playerMaxHealth)
                _playerCurrentHealth = _playerMaxHealth;

            _playerHealthState = (PlayerHealthState) _playerCurrentHealth;

            HandleHeartBeat();
        }

        private void HandleHeartBeat()
        {
            if(_heartBeatAudio == null || !_heartBeatAudio.isPlaying)
                _heartBeatAudio = AudioManager.instance.Play(AudioNameEnum.PLAYER_HEARTBEAT, true);

            switch(_playerHealthState)
            {
                case PlayerHealthState.FINE:
                    _heartBeatAudio.pitch = 0.5f;
                    break;
                case PlayerHealthState.DANGER:
                    _heartBeatAudio.pitch = 0.75f;
                    break;
                case PlayerHealthState.CRITICAL:
                    _heartBeatAudio.pitch = 1f;
                    break;
                case PlayerHealthState.DEAD:
                    _heartBeatAudio.Stop();
                    break;
                default:
                    break;
            }
        }

        private void HandlePlayerDeath()
        {
            AudioManager.instance.Play(AudioNameEnum.PLAYER_DIE);

            PlayerStatesManager.SetPlayerState(PlayerState.DEAD);
            GameStateManager.SetGameState(GameState.GAMEOVER);
        }

        public PlayerHealthState GetPlayerHealthState()
        {
            return _playerHealthState;
        }
    }
}
