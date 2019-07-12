using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class M2M_RemoveUnitHandler : AMHandler<M2M_RemoveUnits>
    {
        protected override void Run(Session session, M2M_RemoveUnits message)
        { 
            Console.WriteLine(" 死亡结算-sessionId/unitId/unittype： " + session.Id + " / " + message.UnitIds.Count + " / " + (UnitType)message.UnitType);
            //1、将服务端Unit 处理一下，如标高为死亡状态，或移出组件管理的字典
            //2、删除 客户端Unit实例或其GameObject

            UnitType unitType = (UnitType)message.UnitType;
            switch (unitType)
            {
                case UnitType.Player:
                    break;
                case UnitType.Monster:
                    //Unit monster = Game.Scene.GetComponent<MonsterUnitComponent>().Get(message.UnitId);

                    M2C_RemoveUnits m2C_RemoveUnits = new M2C_RemoveUnits() { UnitType = message.UnitType };
                    m2C_RemoveUnits.Units.Add(new UnitInfo() { UnitId = message.UnitIds.First() });

                    //UnitInfo unitInfo = new UnitInfo();
                    //unitInfo.UnitId = message.UnitId;
                    //unitInfo.X = monster.Position.x;
                    //unitInfo.Y = monster.Position.y;
                    //unitInfo.Z = monster.Position.z;
                    //m2C_RemoveUnits.Units.Add(unitInfo);

                    MessageHelper.Broadcast(m2C_RemoveUnits);

                    Console.WriteLine(" M2M_RemoveUnitHandler-25-sessionId/unitId/unittype： " + session.Id + " / " + message.UnitIds.First() + " / " + (UnitType)message.UnitType);
                    break;

            }

        }   

    }
}
