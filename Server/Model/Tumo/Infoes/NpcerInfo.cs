using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETModel
{
    public class NpcerInfo : Entity
    {
        public NpcerInfo()
        {
            GetNpcerFromBD();
        }
        public Dictionary<long, Npcer> npcers = new Dictionary<long, Npcer>();

        /// <summary>
        /// 先向 BD 服务器 初始化小怪数据
        /// </summary>
        /// <param name="self"></param>
        void GetNpcerFromBD()
        {
            try
            {
                /// 先向 BD 服务器 初始化小怪数据
                //IPEndPoint mapAddress = StartConfigComponent.Instance.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
                //Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
                //mapSession.Send(new M2M_CreateEnemyUnit() { Count = 4 });

                ///生产 Enemy 数据 Info
                GetEnemys(2);

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
                Npcer npcer = ComponentFactory.CreateWithId<Npcer>(IdGenerater.GenerateId());

                ///20190702
                npcer.AddComponent<NumericComponent>();
                npcer.AddComponent<LifeCDComponent>();
                npcer.spawnPosition = new Vector3(-30 + 20 * i, 0, 30);

                npcer.GetComponent<LifeCDComponent>().lifeCD = 4;
                npcer.GetComponent<LifeCDComponent>().unitType = UnitType.Monster;

                SetNumeric(npcer, new object());

                npcers.Add(npcer.Id, npcer);
            }

            SetPosition(npcers);
        }
        void SetNumeric(Npcer npcer, object obj)
        {
            if (npcer.GetComponent<NumericComponent>() == null) return;
            NumericComponent num = npcer.GetComponent<NumericComponent>();
            /// 二次赋值
            num.Set(NumericType.MaxHpAdd, 20);
        }


        void SetPosition(Dictionary<long, Npcer> npcers)
        {
            List<long> list = new List<long>(npcers.Keys);

            npcers[list[0]].spawnPosition = new Vector3(-35, 0, 5);
            npcers[list[1]].spawnPosition = new Vector3(-25, 0, 5);
        }



    }
}
