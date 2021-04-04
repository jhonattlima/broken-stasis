using System;

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
}
