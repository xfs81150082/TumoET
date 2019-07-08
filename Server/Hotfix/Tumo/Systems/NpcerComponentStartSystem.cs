using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ETHotfix
{
    [ObjectSystem]
    public class NpcerComponentStartSystem : StartSystem<NpcerComponent>
    {
        public override void Start(NpcerComponent self)
        {
            GetEnemyFromBD();

            SetEnemyToMap();

            Console.WriteLine(" NpcerComponentStartSystem-18: " + " BD服务器 Npcer数据；map服务器 实例Npcer： " + Game.Scene.GetComponent<NpcerComponent>().Count);
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

                NpcerInfo enemyInfo = ComponentFactory.Create<NpcerInfo>();
                Game.Scene.GetComponent<NpcerComponent>().AddAll(enemyInfo.npcers.Values.ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// 再向 Map 服务器 初始化小怪实例
        /// </summary>
        void SetEnemyToMap()
        {
            if (Game.Scene.GetComponent<NpcerComponent>().Count > 0)
            {
                /// 再向 Map 服务器 初始化小怪实例
                IPEndPoint mapAddress = StartConfigComponent.Instance.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
                Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);

                foreach (Npcer tem in Game.Scene.GetComponent<NpcerComponent>().GetAll())
                {
                    mapSession.Send(new M2M_CreateNpcerUnit() { NpcerId = tem.Id });
                    Console.WriteLine(" NpcerComponentStartSystem-57: " + " BD服务器 Npcer数据；map服务器 实例Npcer： " + Game.Scene.GetComponent<NpcerComponent>().Count);
                }
            }
        }

    }
}
