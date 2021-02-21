using Utilities;

namespace Enemy
{
    public interface IEnemyAI : IUpdateBehaviour
    {
        void InitializeEnemy();

        void ResetEnemyAI();
    }
}
