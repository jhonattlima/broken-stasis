using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace GameManagers
{
    public class EnemiesManager : MonoBehaviour
    {
        private readonly List<IEnemy> _enemyList;

        public EnemiesManager(List<IEnemy> p_enemyList)
        {
            _enemyList = p_enemyList;
        }

        public void InitializeEnemies()
        {
            _enemyList.ForEach(enemy => enemy.InitializeEnemy());
        }

        public void RunUpdate()
        {
            _enemyList.ForEach(enemy => enemy.RunUpdate());
        }
    }
}