using Audio;
using GameManager;
using UnityEngine;

namespace Enemy
{
    public class BasherStasisAI : IEnemyAI
    {
        private readonly IEnemyAI _basherAI;
        private readonly SensorVision _stasisSensor;
        private bool _isActive = false;

        public BasherStasisAI(IEnemyAI p_basherAI, SensorVision p_stasisSensor)
        {
            _basherAI = p_basherAI;
            _stasisSensor = p_stasisSensor;
        }

        public void InitializeEnemy()
        {
            _isActive = false;
            _stasisSensor.onPlayerDetected += HandleEnemyActivation;
        }

        public void ResetEnemyAI()
        {
            _basherAI.ResetEnemyAI();
        }

        public void RunUpdate()
        {
            if (_isActive)
                _basherAI.RunUpdate();
        }

        private void HandleEnemyActivation(Transform p_playerPosition)
        {
            if (!_isActive)
            {
                AudioManager.instance.Play(AudioNameEnum.BASHER_SCREAM);
                _basherAI.InitializeEnemy();
                _isActive = true;
            }
        }
    }
}
