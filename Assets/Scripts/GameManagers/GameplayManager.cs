using System;
using System.Collections.Generic;
using System.Linq;
using Gameplay.Camera;
using Gameplay.Enemy;
using Gameplay.Objects.Interaction;
using Gameplay.Player;
using Gameplay.Player.Item;
using SaveSystem;
using UnityEngine;

namespace GameManagers
{
    public class GameplayManager : MonoBehaviour
    {
        #region GAME_EVENTS
        public Action<PlayerSuitEnum> onPlayerSuitChange;
        public Action<bool> onActivatePlayerIllumination;
        #endregion

        [SerializeField] private PlayerContainer _playerContainer;
        [SerializeField] private CameraContainer _cameraContainer;
        [SerializeField] private GameObject _levelGameObjects;
        [SerializeField] private GameObject _enemiesGameObjects;

        private PlayerBase _player;
        private CameraFollowPlayer _cameraFollowPlayer;
        private LevelObjectManager _levelObjectManager;
        private EnemiesManager _enemiesManager;
        private LightPriorityManager _lightPriorityManager;
        
        public static GameplayManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;

            RegisterObjectsGraph(_playerContainer);

            _enemiesManager?.InitializeEnemies(_player.onPlayerDamaged);
            
            // NOTE: Not using lightpriorityManager for the moment
            // TODO: Evitar FindObjects (mesmo que chamado só uma vez)
            // _lightPriorityManager = new LightPriorityManager(
            //     GameObject.FindObjectsOfType<Light>().ToList<Light>(),
            //     _playerContainer.playerLights.ToList<Light>()
            // );
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
            onActivatePlayerIllumination = _player?.onActivateIllumination;
        }

        private void FixedUpdate()
        {
            _player?.RunFixedUpdate();
            _cameraFollowPlayer?.RunFixedUpdate();
            _levelObjectManager?.RunFixedUpdate();
            // NOTE: Not using lightpriorityManager for the moment
            // _lightPriorityManager?.RunFixedUpdate();
        }

        private void Update()
        {
            _player?.RunUpdate();
            _levelObjectManager?.RunUpdate();
            _enemiesManager?.RunUpdate();
        }

        //TODO: Transferir para classe adequada (não é papel do GameplayManager)
        private void LoadSaveGame()
        {
            if (!SaveGameManager.LoadGame())
                return;

            ChapterManager.instance.initialChapter = SaveGameManager.gameSaveData.chapter;
            _player.SetPlayerSaveData(SaveGameManager.gameSaveData);

            if (SaveGameManager.gameSaveData.doorsList == null)
                return;

            List<DoorController> __doors = _levelGameObjects.GetComponentsInChildren<DoorController>().ToList();
            foreach (DoorSaveData __savedDoorState in SaveGameManager.gameSaveData.doorsList)
            {
                foreach (DoorController __ingameDoor in __doors)
                {
                    if (__ingameDoor.transform.parent.name == __savedDoorState.parentName)
                    {
                        __ingameDoor.isDoorOpen = __savedDoorState.isDoorOpen;

                        if (__savedDoorState.isDoorLocked)
                            __ingameDoor.LockDoor();
                        else
                            __ingameDoor.UnlockDoor();

                        __ingameDoor.SetDoorState();
                    }
                }
            }
        }

        //TODO: Transferir para classe adequada (não é papel do GameplayManager)
        public GameSaveData GetCurrentGameData()
        {
            GameSaveData __gameSaveData = _player.GetPlayerSaveData();

            __gameSaveData.chapter = ChapterManager.instance.currentChapter.chapterType;

            var __ingameDoorsList = _levelGameObjects.GetComponentsInChildren<DoorController>().ToList();
            __gameSaveData.doorsList = new List<DoorSaveData>();
            foreach (DoorController door in __ingameDoorsList)
            {
                DoorSaveData doorState= new DoorSaveData();
                doorState.parentName = door.transform.parent.name;
                doorState.isDoorOpen = door.isDoorOpen;
                doorState.isDoorLocked = door.isLocked;
                __gameSaveData.doorsList.Add(doorState);
            }

            return __gameSaveData;
        }
    }
}
