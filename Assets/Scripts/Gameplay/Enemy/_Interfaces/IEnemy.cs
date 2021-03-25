using System;
using Utilities;

namespace Gameplay.Enemy
{
    public interface IEnemy : IUpdateBehaviour
    {
        void InitializeEnemy(Action<int> p_onPlayerDamaged);
    }
}