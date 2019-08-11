using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETModel
{
    public class MonsterInfo : Component
    {
        public Dictionary<long, Monster> enemys = new Dictionary<long, Monster>();

        public MonsterInfo()
        {
            GetEnemyFromBD();
        }

        /// <summary>
        /// 先向 BD 服务器 初始化小怪数据
        /// </summary>
        /// <param name="self"></param>
        void GetEnemyFromBD()
        {
            try
            {                
                GetEnemys(4);         ///生产 Enemy 数据 Info

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void GetEnemys(int count)
        {
            for (int i = 0; i < count; i++)
            {
                ///20190702
                Monster enemy = ComponentFactory.CreateWithId<Monster>(IdGenerater.GenerateId());
                enemy.AddComponent<NumericComponent>();
                enemys.Add(enemy.Id, enemy);
            }
            SetPosition(enemys);
        }

        void SetPosition(Dictionary<long, Monster> enemys)
        {
            List<long> list = new List<long>(enemys.Keys);
            enemys[list[0]].spawnPosition = new Vector3(-35, 0, 30);
            enemys[list[1]].spawnPosition = new Vector3(-15, 0, 30);
            enemys[list[2]].spawnPosition = new Vector3(15, 0, 30);
            enemys[list[3]].spawnPosition = new Vector3(35, 0, 30);
        }
    

    }
}
