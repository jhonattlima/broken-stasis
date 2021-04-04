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
        public int playerHealth = 3;
        public Vector3 playerPosition = Vector3.zero;
        public PlayerSuitEnum playerSuit = PlayerSuitEnum.NAKED;
        public IlluminationState playerIlluminationState = new IlluminationState();

        public List<GameSaveDoorState> doorsList = new List<GameSaveDoorState>();

        public ChapterTypeEnum chapter = ChapterTypeEnum.CHAPTER_1;
    }
}
