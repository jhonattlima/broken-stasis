using System;
using System.Collections.Generic;
using UnityEngine;

public interface IChapter
{
    ChapterType chapterType { get; }

    List<IGameEvent> gameEvents { get; }

    event Action onChapterStart;
    event Action onChapterEnd;
}
