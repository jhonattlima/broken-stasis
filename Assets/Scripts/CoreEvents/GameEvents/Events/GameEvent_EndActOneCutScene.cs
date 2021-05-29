using System;
using System.Collections.Generic;
using GameManagers;
using Gameplay.Objects.Interaction;
using UI;
using UnityEngine;
using UnityEngine.Video;
using Utilities;
using Utilities.Audio;
using Utilities.VariableManagement;

namespace CoreEvent.GameEvents
{
    public class GameEvent_EndActOneCutScene : TriggerColliderController, IGameEvent
    {
        [SerializeField] private GameEventTypeEnum _gameEventType;
        [SerializeField] private DoorController _doorController;
        [SerializeField] private List<GameObject> _lightsList;
        [SerializeField] private GameObject _mainCamera;
        [SerializeField] private GameObject _enemiesManager;

        private bool _hasRun;
        private VideoPlayer _videoPlayer;
        private string VIDEO_PATH = VariablesManager.gameplayVariables.cutsceneSplinterProjectVideoPath;

        public GameEventTypeEnum gameEventType { get { return _gameEventType; } }
        public bool hasRun { get { return _hasRun; } }

        private void Awake()
        {
            _hasRun = false;
            onTriggerEnter = HandleOnTriggerEnter;

            _videoPlayer = _mainCamera.AddComponent<VideoPlayer>();
            _videoPlayer.playOnAwake = false;
            _videoPlayer.url = VIDEO_PATH;
            _videoPlayer.renderMode = VideoRenderMode.CameraNearPlane;
            _videoPlayer.loopPointReached += HandleCutSceneEnd;
        }

        private void HandleOnTriggerEnter(Collider other)
        {
            if (other.CompareTag(GameInternalTags.PLAYER) && !_hasRun)
                RunSingleTimeEvents();
        }

        public void RunPermanentEvents() { }

        public void RunSingleTimeEvents()
        {
            _hasRun = true;

            InputController.GamePlay.MouseEnabled = false;
            InputController.GamePlay.InputEnabled = false;

            AudioManager.instance.Stop(AudioNameEnum.SOUND_TRACK_SPLINTER);

            _videoPlayer.Play();

            foreach (GameObject __light in _lightsList)
            {
                __light.SetActive(true);
            }
            DisableAllEnemies();
        }

        private void HandleCutSceneEnd(UnityEngine.Video.VideoPlayer videoPlayer)
        {
            _doorController.isDoorOpen = false;
            _doorController.SetDoorState();
            _doorController.LockDoor();

            _videoPlayer.Stop();

            LoadingView.instance.FadeOut(delegate ()
                {
                    InputController.GamePlay.MouseEnabled = true;
                    InputController.GamePlay.InputEnabled = true;
                }
            , VariablesManager.uiVariables.defaultFadeOutSpeed);
        }

        private void DisableAllEnemies()
        {
            var __childrenObjects = _enemiesManager.GetComponentsInChildren<Transform>();
            List<Transform> __enemies = new List<Transform>(__childrenObjects);
            __enemies.RemoveAt(0);

            foreach (Transform __enemy in __enemies)
                __enemy.gameObject.SetActive(false);
        }
    }
}
