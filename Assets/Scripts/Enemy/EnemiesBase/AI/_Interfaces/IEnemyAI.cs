using Utilities;

namespace Enemy.EnemiesBase
{
    public interface IEnemyAI : IUpdateBehaviour
    {
        void InitializeEnemy();

        void ResetEnemyAI();
    }
}
