using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETModel
{
    public class MonsterInfo : Entity
    {
        public MonsterInfo()
        {
            GetEnemyFromBD();
        }
        public Dictionary<long, Monster> enemys = new Dictionary<long, Monster>();

        /// <summary>
        /// 先向 BD 服务器 初始化小怪数据
        /// </summary>
        /// <param name="self"></param>
        void GetEnemyFromBD()
        {
            try
            {
                ///生产 Enemy 数据 Info
                GetEnemys(4);

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
                Monster enemy = ComponentFactory.CreateWithId<Monster>(IdGenerater.GenerateId());

                ///20190702
                enemy.AddComponent<NumericComponent>();
                enemy.spawnPosition = new Vector3(-35 + 10 * i, 0, 30);


                SetNumeric(enemy, new object());

                enemys.Add(enemy.Id, enemy);
            }

            SetPosition(enemys);
        }
        void SetNumeric(Monster enemy, object obj)
        {
            if (enemy.GetComponent<NumericComponent>() == null) return;
            NumericComponent num = enemy.GetComponent<NumericComponent>();
            /// 二次赋值
            num.Set(NumericType.HpAdd, 80);       // HpAdd 数值,进行赋值
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
