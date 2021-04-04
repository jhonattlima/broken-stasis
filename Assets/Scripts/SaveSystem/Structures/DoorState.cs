using System;

namespace SaveSystem
{
    [Serializable]
    public struct DoorState
    {
        public String parentName;
        public bool isDoorOpen;
        public bool isDoorLocked;
    }
}
