using System;
using System.Collections.Generic;
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

    public List<IGameEvent> gameEvents => throw new NotImplementedException();

    public event Action onChapterStart;
    public event Action onChapterEnd;

    private void Awake()
    {
        onChapterStart = HandleChapterStart;
        onChapterEnd = HandleChapterEnd;
    }

    private void HandleChapterStart()
    {
        LoadingView.instance.FadeOut(delegate ()
        {
            InputController.GamePlay.InputEnabled = true;
        });
    }

    private void HandleChapterEnd()
    {
        
    }
}
