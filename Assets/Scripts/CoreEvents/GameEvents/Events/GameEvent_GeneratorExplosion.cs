using System.Collections.Generic;
using GameManagers;
using Gameplay.Objects.Interaction;
using UnityEngine;
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
        [SerializeField] private GameObject _corridorC5;
        [SerializeField] private GameObject _corridorC5WithBlood;
        [SerializeField] private GameObject _lights;
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
            ChangeGeneratorRoom();
            ChangeCorridorC5();
            TurnOffAllLights();
            OpenDoors();
        }

        public void RunSingleTimeEvents()
        {
            TurnOffAllLights();
            AudioManager.instance.Play(AudioNameEnum.GENERATOR_EXPLOSION, false, delegate ()
            {
                GameHudManager.instance.uiDialogHud.StartDialog(DialogEnum.ACT_03_NO_POWER_WARNING, delegate ()
                {
                    GameHudManager.instance.notificationHud.ShowText("Press [F] to toggle Lantern", 8);
                });
                RunPermanentEvents();

                // Start Chapter 3
                ChapterManager.instance.GoToNextChapter();
            });
            _hasRun = true;
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

        private void TurnOffAllLights()
        {
            _lights.SetActive(false);
        }

        private void OpenDoors()
        {
            List<DoorController> __doorsToOpenAndLock = new List<DoorController>();
            __doorsToOpenAndLock.AddRange(_milestone_1_doors.GetComponentsInChildren<DoorController>());
            __doorsToOpenAndLock.AddRange(_milestone_2_doors.GetComponentsInChildren<DoorController>());
            foreach (DoorController door in __doorsToOpenAndLock)
            {
                door.isDoorOpen = true;
                door.SetDoorState();
                door.LockDoor();
            }
        }
    }
}
