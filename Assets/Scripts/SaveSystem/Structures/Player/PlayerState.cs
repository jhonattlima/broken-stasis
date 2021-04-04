using System;
using Gameplay.Player.Item;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    public struct Playerstate
    {
        public int health;
        public Vector3 position;
        public PlayerSuitEnum suit;
        public PlayerIlluminationState playerIlluminationState;
    }
}
