using System;
using System.Collections;
using System.Collections.Generic;
using Interaction;
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

        public ChapterType chapter = ChapterType.CHAPTER_1;
    }
}
