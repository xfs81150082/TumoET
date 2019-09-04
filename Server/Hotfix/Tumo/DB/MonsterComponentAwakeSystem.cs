using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class MonsterComponentAwakeSystem : AwakeSystem<MonsterComponent>
    {      
        public override void Awake(MonsterComponent self)
        {
            ///先向 BD 服务器 初始化小怪数据
            GetEnemyFromBD();
        }

        /// <summary>
        /// 先向 BD 服务器 初始化小怪数据
        /// </summary>
        void GetEnemyFromBD()
        {
            try
            {
                /// 先向 BD 服务器 读取数据 初始化小怪数据
                List<long> ids = new List<long>() { 11101, 11102, 11103, 11104 };

                QueryMonsterdb(ids).Coroutine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static async ETVoid QueryMonsterdb(List<long> ids)
        {
            DBProxyComponent dBProxy = Game.Scene.GetComponent<DBProxyComponent>();

            Console.WriteLine(" MonsterComponentStartSystem-42 " );

            List<ComponentWithId> testOnes = await dBProxy.Query<Monsterdb>(ids);

            Console.WriteLine(" MonsterComponentStartSystem-59: " + testOnes.Count);

            foreach(Monsterdb tem in testOnes)
            {
                Monster monster = tem.ToMonster();
        
                /// 然后向 Gate 服务器 小怪数据放入字典                
                Game.Scene.GetComponent<MonsterComponent>().Add(monster);
            }

            Console.WriteLine(" MonsterComponentStartSystem-60: " + " BD服务器，小怪数量： " + Game.Scene.GetComponent<MonsterComponent>().Count);
        }
    }

    public static class MonsterHelper
    {
        public static Monster ToMonster(this Monsterdb self)
        {
            Monster monster = ComponentFactory.CreateWithId<Monster>(self.Id);
            monster.spawnPosition = new Vector3((float)self.spawnVec[0], (float)self.spawnVec[1], (float)self.spawnVec[2]);
            return monster;
        }

        public static Monsterdb ToMonsterdb(this Monster self)
        {
            Monsterdb monsterdb = ComponentFactory.CreateWithId<Monsterdb>(self.Id);
            monsterdb.spawnVec.Add(self.spawnPosition.x);
            monsterdb.spawnVec.Add(self.spawnPosition.y);
            monsterdb.spawnVec.Add(self.spawnPosition.z);
            monsterdb.level = 10;
            monsterdb.exp = 0;
            monsterdb.hp = 40;
            monsterdb.name = "第六题";
            return monsterdb;
        }

    }



}
