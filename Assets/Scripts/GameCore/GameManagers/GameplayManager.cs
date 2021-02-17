using System;
using System.Collections.Generic;
using System.Linq;
using CameraScripts;
using Enemy;
using Interaction;
using Player;
using SaveSystem;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace GameManagers
{
    public class GameplayManager : MonoBehaviour
    {
# region GAME_EVENTS
        public Action<PlayerSuitEnum> onPlayerSuitChange;
#endregion

        [SerializeField] private PlayerContainer _playerContainer;
        [SerializeField] private CameraContainer _cameraContainer;
        [SerializeField] private GameObject _levelGameObjects;
        [SerializeField] private GameObject _enemiesGameObjects;

        private PlayerBase _player;
        private CameraFollowPlayer _cameraFollowPlayer;
        private LevelObjectManager _levelObjectManager;
        private EnemiesManager _enemiesManager;
        public static GameplayManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            RegisterObjectsGraph(_playerContainer);

            _enemiesManager?.InitializeEnemies(_player.onPlayerDamaged);
        }

        private void Start()
        {
            // TODO: Transferir lógica de load e inicialização para classe superior
            LoadSaveGame();
            ChapterManager.instance?.InitializeChapters();
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
            
            onPlayerSuitChange = _player?.onSuitChange;
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

        //TODO: Transferir para classe adequada (não é papel do GameplayManager)
        private void LoadSaveGame()
        {
            SaveGameManager.LoadGame();

            if(SaveGameManager.gameSaveData == null) return;
            
            ChapterManager.instance.initialChapter = SaveGameManager.gameSaveData.chapter;
            _player.SetPlayerSaveData(SaveGameManager.gameSaveData);
            
            List<DoorController> __doors = _levelGameObjects.GetComponentsInChildren<DoorController>().ToList();
            if(__doors.Count > 0 && SaveGameManager.gameSaveData.doorsList.Count > 0)
            {
                for(int i = 0; i < __doors.Count; i++)
                {
                    __doors[i].isDoorOpen = SaveGameManager.gameSaveData.doorsList[i].isDoorOpen;
                }
            }
        }

        //TODO: Transferir para classe adequada (não é papel do GameplayManager)
        public GameSaveData GetCurrentGameData()
        {
            GameSaveData __gameSaveData = _player.GetPlayerSaveData();

            __gameSaveData.chapter = ChapterManager.instance.currentChapter.chapterType;
            __gameSaveData.doorsList = _levelGameObjects.GetComponentsInChildren<DoorController>().ToList();

            return __gameSaveData;
        }
    }
}
