﻿using System;
using System.Collections.Generic;
using Gameplay.Enemy;
using UnityEngine;
using Utilities;

namespace GameManagers
{
    public class EnemiesManager : MonoBehaviour, IUpdateBehaviour
    {
        private readonly List<IEnemy> _enemyList;

        public EnemiesManager(List<IEnemy> p_enemyList)
        {
            _enemyList = p_enemyList;
        }

        public void InitializeEnemies(Action<int> p_onPlayerDamaged)
        {
            _enemyList.ForEach(enemy => enemy.InitializeEnemy(p_onPlayerDamaged));
        }

        public void RunUpdate()
        {
            _enemyList.ForEach(enemy => enemy.RunUpdate());
        }
    }
}