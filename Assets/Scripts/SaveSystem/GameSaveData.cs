using System;
using System.Collections;
using System.Collections.Generic;
using Interaction;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public class GameSaveDoorState
    {
        public String parentName;
        public bool isDoorOpen;
        public bool isDoorLocked;

        public GameSaveDoorState(String p_parentName, bool p_isDoorOpen, bool p_isDoorLocked)
        {
            this.parentName = p_parentName;
            this.isDoorOpen = p_isDoorOpen;
            this.isDoorLocked = p_isDoorLocked;
        }
    }

    [Serializable]
    public class GameSaveData
    {
        public int playerHealth = 3;
        public Vector3 playerPosition = Vector3.zero;
        public PlayerSuitEnum playerSuit = PlayerSuitEnum.NAKED;

        public List<GameSaveDoorState> doorsList = new List<GameSaveDoorState>();

        public ChapterType chapter = ChapterType.CHAPTER_1;
    }
}
