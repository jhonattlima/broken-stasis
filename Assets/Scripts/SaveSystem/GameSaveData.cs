using System;
using System.Collections.Generic;
using CoreEvent.Chapters;
using Gameplay.Player.Item;
using UnityEngine;

namespace SaveSystem
{

    [Serializable]
    public class GameSaveData
    {
        public Playerstate playerstate;
        public List<DoorState> doorsList = new List<DoorState>();
        public ChapterTypeEnum chapter = ChapterTypeEnum.CHAPTER_1;
    }
}
