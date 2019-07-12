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
    public class EnemyComponentStartSystem : StartSystem<EnemyComponent>
    {      
        public override void Start(EnemyComponent self)
        {
            GetEnemyFromBD();

            SetEnemyToMap().Coroutine(); ;

            Console.WriteLine(" EnemyComponentStartSystem-20: " + " BD服务器 小怪数据；map服务器 实例小怪： " + Game.Scene.GetComponent<EnemyComponent>().Count);
        }

        /// <summary>
        /// 先向 BD 服务器 初始化小怪数据
        /// </summary>
        void GetEnemyFromBD()
        {
            try
            {
                /// 先向 BD 服务器 初始化小怪数据
                //IPEndPoint mapAddress = StartConfigComponent.Instance.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
                //Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
                //mapSession.Send(new M2M_CreateEnemyUnit() { Count = 4 });

                EnemyInfo enemyInfo = ComponentFactory.Create<EnemyInfo>();
                Game.Scene.GetComponent<EnemyComponent>().AddAll(enemyInfo.enemys.Values.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 再向 Map 服务器 初始化小怪实例
        /// </summary>
        async ETVoid SetEnemyToMap()
        {
            if (Game.Scene.GetComponent<EnemyComponent>().Count > 0)
            {
                /// 再向 Map 服务器 初始化小怪实例
                foreach (Enemy tem in Game.Scene.GetComponent<EnemyComponent>().GetAll())
                {
                    M2MM_CreateEnemyUnit responseUnit = (M2MM_CreateEnemyUnit)await MapSessionHelper.Session().Call(new M2M_CreateEnemyUnit() { EnemyId = tem.Id });

                    tem.UnitId = responseUnit.UnitId;
                }
            }
        }

    }
}