using System.Collections.Generic;
using System.Linq;
using CoreEvent.GameEvents;
using GameManagers;
using SaveSystem;
using UI.ToolTip;
using UnityEngine;
using Utilities;

namespace CoreEvent.Chapters
{
    public class Chapter_2 : MonoBehaviour, IChapter
    {
        [SerializeField] private ChapterTypeEnum _chapterType;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private CharacterController _playerCharacterController;
        [SerializeField] private ToolTip _generatorToolTip;
        [SerializeField] private ToolTip[] _deactivatedToolTips;

        public ChapterTypeEnum chapterType
        {
            get
            {
                return _chapterType;
            }
        }

        public List<IGameEvent> gameEvents
        {
            get
            {
                return gameObject.GetComponentsInChildren<IGameEvent>().ToList();
            }
        }

        public void ChapterStart()
        {
            Debug.Log("STARTED CHAPTER 2");

            _generatorToolTip.ActivateToolTip();

            foreach(ToolTip __tooltip in _deactivatedToolTips)
            {
                __tooltip.DeactivateTooltip();
            }

            _playerCharacterController.enabled = false;
            _playerCharacterController.transform.position = _startPosition;
            _playerCharacterController.enabled = true;

            if (SaveGameManager.gameSaveData.chapter != chapterType)
            {
                SaveGameManager.gameSaveData = GameplayManager.instance.GetCurrentGameData();
                SaveGameManager.SaveGame();
            }

            InputController.GamePlay.InputEnabled = true;
            InputController.GamePlay.MouseEnabled = true;
        }

        public void ChapterEnd()
        {
            Debug.Log("FINISHED CHAPTER 2");
        }
    }
}
