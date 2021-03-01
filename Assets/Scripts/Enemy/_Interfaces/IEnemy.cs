using System;
using Utilities;

namespace Enemy
{
    public interface IEnemy : IUpdateBehaviour
    {
        void InitializeEnemy(Action<int> p_onPlayerDamaged);
    }
}