using System.Collections.Generic;
using System.Linq;
using CoreEvent.GameEvents;
using GameManagers;
using Gameplay.Objects.Items;
using SaveSystem;
using UnityEngine;
using Utilities;

namespace CoreEvent.Chapters
{
    public class Chapter_3 : MonoBehaviour, IChapter
    {
        [SerializeField] private ChapterTypeEnum _chapterType;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private CharacterController _playerCharacterController;
        [SerializeField] private ItemKeyCard _itemKeyCard;

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
            Debug.Log("STARTED CHAPTER 3");

            _playerCharacterController.enabled = false;
            _playerCharacterController.transform.position = _startPosition;
            _playerCharacterController.enabled = true;

            if (SaveGameManager.gameSaveData.chapter != chapterType)
            {
                SaveGameManager.gameSaveData = GameplayManager.instance.GetCurrentGameData();

                SaveGameManager.SaveGame();
            }
            InputController.GamePlay.InputEnabled = true;

            _itemKeyCard.SetEnabled(true);
            // TODO
            // Activate surprise basher
        }

        public void ChapterEnd()
        {
            Debug.Log("FINISHED CHAPTER 3");
        }
    }
}
