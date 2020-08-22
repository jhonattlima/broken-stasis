using System;

namespace Enemy
{
    public enum EnemyState
    {
        IDLE,
        PATROLLING,
        INVESTIGATING,
        RUNNING,
        ATTACKING
    }

    public class EnemyStatesManager
    {
        public EnemyState currentState { get; private set;}

        public Action<EnemyState> onStateChanged;

        public void SetEnemyState (EnemyState p_newState)
        {
            if(!currentState.Equals(p_newState))
            {
                currentState = p_newState;
                UnityEngine.Debug.Log(p_newState);
                if (onStateChanged != null) onStateChanged(currentState);
            }
        }
    }
}
