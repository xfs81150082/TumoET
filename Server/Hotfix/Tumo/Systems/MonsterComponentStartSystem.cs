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
    public class MonsterComponentStartSystem : StartSystem<MonsterComponent>
    {      
        public override void Start(MonsterComponent self)
        {
            ///先向 BD 服务器 初始化小怪数据
            GetEnemyFromBD();

            ///向 map 服务器 实例小怪
            CreateMonsterToMap(Game.Scene.GetComponent<MonsterComponent>().GetAll());
        }

        /// <summary>
        /// 先向 BD 服务器 初始化小怪数据
        /// </summary>
        void GetEnemyFromBD()
        {
            try
            {
                /// 先向 BD 服务器 初始化小怪数据 ///ToTo               
                MonsterInfo enemyInfo = ComponentFactory.Create<MonsterInfo>();

                /// 然后向 Gate 服务器 小怪数据放入字典                
                Game.Scene.GetComponent<MonsterComponent>().AddAll(enemyInfo.enemys.Values.ToArray());

                Console.WriteLine(" MonsterComponentStartSystem-36: " + " BD服务器，小怪数量： " + Game.Scene.GetComponent<MonsterComponent>().Count);
                Console.WriteLine(" MonsterComponentStartSystem-37: " + " map服务器，实例小怪数量： " + Game.Scene.GetComponent<MonsterUnitComponent>().Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 再向 Map 服务器 初始化小怪实例
        /// </summary>
        void CreateMonsterToMap(Monster[] monsters)
        {
            /// 再向 Map 服务器 初始化小怪实例
            if (monsters.Length > 0)
            {
                /// 再向 Map 服务器 初始化小怪实例
                foreach (Monster tem in monsters)
                {
                    SpawnUnit(tem).Coroutine();

                    //M2G_CreateUnit response = (M2G_CreateUnit)await SessionHelper.MapSession().Call(new G2M_CreateUnit() { UnitType = (int)UnitType.Monster, RolerId = tem.Id });
                    //tem.UnitId = response.UnitId;
                }
            }
            Console.WriteLine(" MonsterComponentStartSystem-60: " + " BD服务器，小怪数量： " + Game.Scene.GetComponent<MonsterComponent>().Count);
            Console.WriteLine(" MonsterComponentStartSystem-61: " + " map服务器，实例小怪数量： " + Game.Scene.GetComponent<MonsterUnitComponent>().Count);
        }

        async ETVoid SpawnUnit(Monster monster)
        {
            M2G_CreateUnit response = (M2G_CreateUnit)await SessionHelper.MapSession().Call(new G2M_CreateUnit() { UnitType = (int)UnitType.Monster, RolerId = monster.Id });
            monster.UnitId = response.UnitId;
        }

    }
}