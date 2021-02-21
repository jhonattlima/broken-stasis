using System;
using System.Collections.Generic;
using UnityEngine;

namespace CoreEvent
{
    public interface IChapter
    {
        ChapterTypeEnum chapterType { get; }
        List<IGameEvent> gameEvents { get; }

        void ChapterStart();
        void ChapterEnd();
    }
}
