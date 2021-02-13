using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]  
    public class GameSaveData
    {
        public int playerHealth = 3;
        public Vector3 playerPosition = Vector3.zero;
        public PlayerSuitEnum playerSuit = PlayerSuitEnum.NAKED;

        public ChapterType chapter = ChapterType.CHAPTER_1;
    }
}
