using UnityEngine;

namespace Enemy
{
    public interface IFollowEnemy
    {
        void InvestigatePosition(Transform p_destinationPosition);
        void SprintToPosition(Transform p_destinationPosition);
    }
}