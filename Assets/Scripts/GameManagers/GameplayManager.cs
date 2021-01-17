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
        [SerializeField] private PlayerContainer _playerContainer;
        [SerializeField] private CameraContainer _cameraContainer;
        [SerializeField] private GameObject _levelGameObjects;
        [SerializeField] private GameObject _enemiesGameObjects;

        private PlayerBase _player;
        private CameraFollowPlayer _cameraFollowPlayer;
        private LevelObjectManager _levelObjectManager;
        private EnemiesManager _enemiesManager;
        public static GameplayManager instance;
        private PlayerSuitEnum _playerSuitEnum;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            LoadingView.instance.FadeOut(delegate ()
            {
                InputController.GamePlay.InputEnabled = true;
            });

            RegisterObjectsGraph(_playerContainer);

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

            _player?.InitializePlayer();
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

        public void ChangePlayerSuit(PlayerSuitEnum p_playerSuitEnum)
        {
            if (p_playerSuitEnum == _playerSuitEnum) return;

            switch (p_playerSuitEnum)
            {
                case PlayerSuitEnum.NAKED:
                {
                    _player.RegisterPlayerAnimator(_playerContainer.nakedAnimator, _playerContainer.suit1AnimationEventHandler);
                    _playerContainer.suit1GameObject.SetActive(false);
                    _playerContainer.nakedGameObject.SetActive(true);
                    break;
                }
                case PlayerSuitEnum.SUIT1:
                {
                    _player.RegisterPlayerAnimator(_playerContainer.suit1Animator, _playerContainer.suit1AnimationEventHandler);
                    _playerContainer.nakedGameObject.SetActive(false);
                    _playerContainer.suit1GameObject.SetActive(true);
                    break;
                }
            }
            _playerSuitEnum = p_playerSuitEnum;
        }
    }
}
