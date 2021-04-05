using System;
using System.Collections.Generic;
using CoreEvent.Chapters;
using SaveSystem.Player;

namespace SaveSystem
{

    [Serializable]
    public class GameSaveData
    {
        public PlayerSaveData playerData;
        public List<DoorSaveData> doorsList = new List<DoorSaveData>();
        public ChapterTypeEnum chapter = ChapterTypeEnum.CHAPTER_1;
    }
}
