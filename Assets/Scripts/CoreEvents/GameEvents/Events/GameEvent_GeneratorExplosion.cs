using System.Collections.Generic;
using GameManagers;
using Gameplay.Lighting;
using Gameplay.Objects.Interaction;
using Gameplay.Objects.Items;
using Gameplay.Scenario;
using UnityEngine;
using Utilities;
using Utilities.Audio;
using Utilities.UI;

namespace CoreEvent.GameEvents
{
    public class GameEvent_GeneratorExplosion : MonoBehaviour, IGameEvent
    {
        [SerializeField] private GameEventTypeEnum _gameEventType;
        [SerializeField] private GameObject _roomGenerator;
        [SerializeField] private GameObject _generatorInterface;
        [SerializeField] private GameObject _roomGeneratorExploded;
        [SerializeField] private ItemFlashlight _itemFlashlight;
        [SerializeField] private GameObject _corridorC5;
        [SerializeField] private GameObject _corridorC5WithBlood;
        [SerializeField] private List<GameObject> _allLights;
        [SerializeField] private List<CoverController> _scenarioCovers;
        [SerializeField] private GameObject _dressingRoomLights;
        [SerializeField] private GameObject _milestone_1_doors;
        [SerializeField] private GameObject _milestone_2_doors;
        [SerializeField] private GameObject _environmentLightExplosion;
        [SerializeField] private GameObject _eventLightBlink;

        public GameEventTypeEnum gameEventType
        {
            get
            {
                return _gameEventType;
            }
        }

        private bool _hasRun;
        public bool hasRun
        {
            get
            {
                return _hasRun;
            }
        }

        private void Awake()
        {
            _hasRun = false;
        }

        public void RunPermanentEvents()
        {
            _environmentLightExplosion.SetActive(false);
            _eventLightBlink.SetActive(false);
            _itemFlashlight.SetCollected(true);
            ChangeGeneratorRoom();
            ChangeCorridorC5();
            TurnOffAllLights();
            DisableCovers();
            OpenDoors();
        }

        public void RunSingleTimeEvents()
        {
            GameStateManager.SetGameState(GameState.CUTSCENE);
            BlinkLights();
            AudioManager.instance.Play(AudioNameEnum.GENERATOR_ELETRIC_OVERCHARGE, false, delegate ()
            {
                TFWToolKit.StartCoroutine(CameraShakerController.Shake(3, 5f, 1));
                AudioManager.instance.Play(AudioNameEnum.GENERATOR_EXPLOSION, false, delegate ()
                {
                    TurnOffAllLights();
                    GameHudManager.instance.uiDialogHud.StartDialog(DialogEnum.ACT_03_NO_POWER_WARNING, delegate ()
                    {
                        GameHudManager.instance.notificationHud.ShowText("Press [F] to toggle Lantern", 8);
                        _hasRun = true;
                        RunPermanentEvents();

                        // Start Chapter 3
                        ChapterManager.instance.GoToNextChapter();
                        GameStateManager.SetGameState(GameState.RUNNING);
                    });
                });
            });
        }

        private void ChangeGeneratorRoom()
        {
            _roomGenerator.SetActive(false);
            _generatorInterface.SetActive(false);
            _roomGeneratorExploded.SetActive(true);
        }

        private void ChangeCorridorC5()
        {
            _corridorC5.SetActive(false);
            _corridorC5WithBlood.SetActive(true);
        }

        private void BlinkLights()
        {
            foreach (LightController __light in _dressingRoomLights.GetComponentsInChildren<LightController>())
            {
                __light.SetLightState(LightEnum.LOW_ILUMINATION_FLICKING);
            }
        }

        private void TurnOffAllLights()
        {
            foreach (GameObject __light in _allLights)
                __light.SetActive(false);
        }

        private void DisableCovers()
        {
            foreach(CoverController __cover in _scenarioCovers)
                __cover.DisableCover();
        }

        private void OpenDoors()
        {
            List<DoorController> __doorsToOpenAndLock = new List<DoorController>();
            __doorsToOpenAndLock.AddRange(_milestone_1_doors.GetComponentsInChildren<DoorController>());
            __doorsToOpenAndLock.AddRange(_milestone_2_doors.GetComponentsInChildren<DoorController>());
            foreach (DoorController __door in __doorsToOpenAndLock)
            {
                __door.isDoorOpen = true;
                __door.SetDoorState();
                __door.LockDoor();
            }
        }
    }
}
