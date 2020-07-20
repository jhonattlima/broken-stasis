using UnityEngine;

namespace Enemy
{
    public interface IAttackMeleeEnemy
    {
        bool CanAttack(Vector3 p_playerPosition);
        void RunUpdate();
    }
}
