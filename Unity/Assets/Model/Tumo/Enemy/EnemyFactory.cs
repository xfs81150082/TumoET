using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ETModel
{
    public static class EnemyFactory
    {
        public static Enemy Create(long id)
        {
            Enemy enemy = ComponentFactory.CreateWithId<Enemy>(id);
            EnemyComponent enemyComponent = Game.Scene.GetComponent<EnemyComponent>();
            enemyComponent.Add(enemy);
            return enemy;
        }
    }
}