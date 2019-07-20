using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ETModel
{
    public static class MonsterFactory
    {
        public static Monster Create(long id)
        {
            Monster enemy = ComponentFactory.CreateWithId<Monster>(id);
            MonsterComponent enemyComponent = Game.Scene.GetComponent<MonsterComponent>();
            enemyComponent.Add(enemy);
            return enemy;
        }
    }
}