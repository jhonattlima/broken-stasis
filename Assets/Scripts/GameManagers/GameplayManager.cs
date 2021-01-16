using System.Linq;
using CameraScripts;
using Enemy;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace GameManagers
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private PlayerContainer _nakedPlayerContainer;
        [SerializeField] private PlayerContainer _suit1PlayerContainer;
        [SerializeField] private CameraContainer _cameraContainer;
        [SerializeField] private GameObject _levelGameObjects;
        [SerializeField] private GameObject _enemiesGameObjects;

        private PlayerBase _player;
        private CameraFollowPlayer _cameraFollowPlayer;
        private LevelObjectManager _levelObjectManager;
        private EnemiesManager _enemiesManager;
        private PlayerSuitEnum _playerSuitEnum;
        public static GameplayManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            LoadingView.instance.FadeOut(delegate ()
            {
                InputController.GamePlay.InputEnabled = true;
            });

            RegisterObjectsGraph(_nakedPlayerContainer);
            _playerSuitEnum = PlayerSuitEnum.NAKED;

            _player?.InitializePlayer();
            _enemiesManager?.InitializeEnemies(_player.onPlayerDamaged);
        }

        private void RegisterObjectsGraph(PlayerContainer p_playercontainer)
        {
            RegisterPlayerGraph(p_playercontainer);
            if (_levelGameObjects != null) _levelObjectManager = new LevelObjectManager(_levelGameObjects.GetComponentsInChildren<IInteractionObject>().ToList());
            if (_enemiesGameObjects != null) _enemiesManager = new EnemiesManager(_enemiesGameObjects.GetComponentsInChildren<IEnemy>().ToList());
        }

        private void RegisterPlayerGraph(PlayerContainer p_playercontainer)
        {
            if (p_playercontainer != null) _player = new PlayerBase(p_playercontainer);
            if (p_playercontainer != null && _cameraContainer != null) _cameraFollowPlayer = new CameraFollowPlayer(p_playercontainer.playerTransform, _cameraContainer.cameraTransform);
        }

        private void FixedUpdate()
        {
            _player?.RunFixedUpdate();
            _cameraFollowPlayer?.RunFixedUpdate();
            _levelObjectManager?.RunFixedUpdate();
        }

        private void Update()
        {
            _levelObjectManager?.RunUpdate();
            _enemiesManager?.RunUpdate();
        }

        public void ChangePlayerGameObject(PlayerSuitEnum p_playersuitEnum)
        {
            if(p_playersuitEnum == _playerSuitEnum) return;

            switch (p_playersuitEnum)
            {
                case PlayerSuitEnum.NAKED:
                {
                    break;
                }
                case PlayerSuitEnum.SUIT1:
                {
                    break;
                }
            }
            _playerSuitEnum = p_playersuitEnum;
        }
    }
}
