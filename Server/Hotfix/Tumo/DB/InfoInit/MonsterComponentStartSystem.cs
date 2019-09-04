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
            //GetEnemyFromBD();
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

                Console.WriteLine(" MonsterComponentStartSystem-33: " + " BD服务器，小怪数量： " + Game.Scene.GetComponent<MonsterComponent>().Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        
        
    }
}
