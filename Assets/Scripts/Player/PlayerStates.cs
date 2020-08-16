using System;

namespace Player
{
    public enum PlayerState
    {
        STATIC,
        WALKING_FORWARD,
        WALKING_SIDEWAYS,
        WALKING_BACKWARD,
        RUNNING_FORWARD,
        RUNNING_SIDEWAYS,
        DEAD,
        HIT,
        PRESS_BUTTON
    }

    public static class PlayerStatesManager
    {
        public static PlayerState currentState { get; private set;}
        public static Action<PlayerState> onStateChanged;
        public static Action<bool> onPlayerCrouching;

        public static void SetPlayerState (PlayerState p_newState)
        {
            if(currentState != p_newState)
            {
                currentState = p_newState;
                
                if (onStateChanged != null) onStateChanged(currentState);
            }
        }
    }
}
