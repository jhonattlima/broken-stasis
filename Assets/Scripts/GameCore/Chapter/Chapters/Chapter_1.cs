using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

public class Chapter_1 : MonoBehaviour, IChapter
{
    [SerializeField] private ChapterType _chapterType;
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
        LoadingView.instance.FadeOut(delegate ()
        {
            InputController.GamePlay.InputEnabled = true;
        });
    }

    public void ChapterEnd()
    {
        Debug.Log("FINISHED CHAPTER 1");
    }
}
