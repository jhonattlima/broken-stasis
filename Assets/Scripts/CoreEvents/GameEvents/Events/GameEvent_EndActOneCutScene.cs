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
        [SerializeField] private CharacterController _playerCharacterController;
        [SerializeField] private Animator _corridorC8Animator;
        [SerializeField] private Vector3 _inRoomPosition;

        private bool _hasRun;

        public bool hasRun { get { return _hasRun; } }
        public GameEventTypeEnum gameEventType { get { return _gameEventType; } }

        private void Awake()
        {
            _hasRun = false;
        }

        public void RunPermanentEvents() { }

        public void RunSingleTimeEvents()
        {
            if (_hasRun) return;

            AudioManager.instance.StopMusic(2.0f);

            GameStateManager.SetGameState(GameState.CUTSCENE);

            VideoController.instance.PlayVideo(VariablesManager.videoVariables.splinterEscape, VariablesManager.videoVariables.splinterEscapeVolume, HandleCutSceneEnd);
            foreach (GameObject __light in _lightsList)
            {
                __light.SetActive(true);
            }
            DisableAllEnemies();
        }

        private void HandleCutSceneEnd()
        {
            _doorController.isDoorOpen = false;
            _doorController.SetDoorState();
            _doorController.LockDoor();

            _playerCharacterController.enabled = false;
            _playerCharacterController.transform.position = _inRoomPosition;
            _corridorC8Animator.Play("FadeOut");
            _playerCharacterController.enabled = true;
            
            _hasRun = true;
            
            LoadingView.instance.FadeOut(null);

            GameStateManager.SetGameState(GameState.RUNNING);
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
