using System.Collections.Generic;
using System.Linq;
using GameManagers;
using SaveSystem;
using UnityEngine;
using Utilities;

public class Chapter_2 : MonoBehaviour, IChapter
{
    [SerializeField] private ChapterType _chapterType;
    [SerializeField] private Vector3 _startPosition;
    [SerializeField] private CharacterController _playerCharacterController;
    
    
    public ChapterType chapterType 
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

        _playerCharacterController.enabled = false;
        _playerCharacterController.transform.position = _startPosition;
        _playerCharacterController.enabled = true;

        if(SaveGameManager.gameSaveData.chapter != chapterType)
        {
            SaveGameManager.gameSaveData = GameplayManager.instance.GetCurrentGameData();
            SaveGameManager.SaveGame();
        }


        InputController.GamePlay.InputEnabled = true;
    }

    public void ChapterEnd()
    {
        Debug.Log("FINISHED CHAPTER 2");
    }
}
