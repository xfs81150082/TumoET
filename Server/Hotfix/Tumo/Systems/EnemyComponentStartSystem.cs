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
    public class EnemyComponentStartSystem : StartSystem<MonsterComponent>
    {      
        public override void Start(MonsterComponent self)
        {
            GetEnemyFromBD();

            ///向 map 服务器 实例小怪
            SetToMap(); 

            Console.WriteLine(" EnemyComponentStartSystem-20: " + " BD服务器 小怪数据；map服务器 实例小怪： " + Game.Scene.GetComponent<MonsterComponent>().Count);
        }

        /// <summary>
        /// 先向 BD 服务器 初始化小怪数据
        /// </summary>
        void GetEnemyFromBD()
        {
            try
            {
                /// 先向 BD 服务器 初始化小怪数据
                ///ToTo
                MonsterInfo enemyInfo = ComponentFactory.Create<MonsterInfo>();

                /// 然后向 Gate 服务器 小怪数据放入字典                
                Game.Scene.GetComponent<MonsterComponent>().AddAll(enemyInfo.enemys.Values.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 再向 Map 服务器 初始化小怪实例
        /// </summary>
        void SetToMap()
        {
            /// 再向 Map 服务器 初始化小怪实例
            if (Game.Scene.GetComponent<MonsterComponent>().Count > 0)
            {
                /// 再向 Map 服务器 初始化小怪实例
                foreach (Monster tem in Game.Scene.GetComponent<MonsterComponent>().GetAll())
                {
                    //MapSessionHelper.Session().Send(new M2M_CreateUnit() { UnitType = (int)UnitType.Monster, RolerId = tem.Id });
                    MapSessionHelper.Session().Send(new G2M_CreateUnit() { UnitType = (int)UnitType.Monster, RolerId = tem.Id });
                }
            }
            Console.WriteLine(" EnemyComponentStartSystem-58: " + " 向 map服务器 实例小怪： " + Game.Scene.GetComponent<MonsterComponent>().Count);
        }


    }
}