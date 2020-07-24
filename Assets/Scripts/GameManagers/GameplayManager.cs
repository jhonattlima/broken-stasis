using System.Linq;
using CameraScripts;
using Enemy;
using Player;
using UnityEngine;

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

        private void Awake()
        {
            RegisterObjectsGraph();

            _player?.InitializePlayer();
            _enemiesManager?.InitializeEnemies(_player.onPlayerDamaged);
        }

        private void RegisterObjectsGraph()
        {
            if(_playerContainer != null) _player = new PlayerBase(_playerContainer);
            if(_playerContainer != null && _cameraContainer != null) _cameraFollowPlayer = new CameraFollowPlayer(_playerContainer.playerTransform, _cameraContainer.cameraTransform);
            if(_levelGameObjects != null) _levelObjectManager = new LevelObjectManager(_levelGameObjects.GetComponentsInChildren<IInteractionObject>().ToList());
            if(_enemiesGameObjects != null) _enemiesManager = new EnemiesManager(_enemiesGameObjects.GetComponentsInChildren<IEnemy>().ToList());
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
    }
}
