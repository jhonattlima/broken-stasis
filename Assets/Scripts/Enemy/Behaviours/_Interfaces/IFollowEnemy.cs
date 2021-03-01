using UnityEngine;

namespace Enemy.Behaviours
{
    public interface IFollowEnemy
    {
        void InvestigatePosition(Transform p_destinationPosition);
        void SprintToPosition(Transform p_destinationPosition);
        void RunFollowEnemy();
    }
}