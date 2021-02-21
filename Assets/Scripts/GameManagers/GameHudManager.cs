using UI;
using UnityEngine;

namespace GameManager
{
    public class GameHudManager : MonoBehaviour
    {
        public NotificationUI itemCollectedHud;

        public static GameHudManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }
    }
}