using System;
using System.Collections.Generic;
using CoreEvent;
using Interaction;
using Player;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class GameSaveData
    {
        public int playerHealth = 3;
        public Vector3 playerPosition = Vector3.zero;
        public PlayerSuitEnum playerSuit = PlayerSuitEnum.NAKED;

        public List<DoorController> doorsList = new List<DoorController>();

        public ChapterTypeEnum chapter = ChapterTypeEnum.CHAPTER_1;
    }
}
