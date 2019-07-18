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
            Console.WriteLine(" M2M_RemoveUnitHandler-15-unittype/uis/pis:  " + (UnitType)message.UnitType + " / " + message.UnitIds.Count + " / " + message.PlayerUnitIds.Count);
            //1、将服务端Unit 处理一下，如标高为死亡状态，或移出组件管理的字典
            //2、删除 客户端Unit实例或其GameObject

            int unitType = message.UnitType;
            switch (unitType)
            {
                case 0:
                    M2C_RemoveUnits m2C_RemoveUnits0 = new M2C_RemoveUnits() { UnitType = unitType };
                    foreach (long monsterId in message.UnitIds)
                    {
                        UnitInfo unitInfo = new UnitInfo();
                        unitInfo.UnitId = monsterId;
                        m2C_RemoveUnits0.Units.Add(unitInfo);

                    }
                    MessageHelper.Broadcast(m2C_RemoveUnits0, message.PlayerUnitIds.ToArray());

                    Console.WriteLine(" M2M_RemoveUnitHandler-25-sessionId/unitId/unittype： " + session.Id + " / " + message.UnitIds.First() + " / " + (UnitType)message.UnitType);
                    break;
                case 1:
                    M2C_RemoveUnits m2C_RemoveUnits1 = new M2C_RemoveUnits() { UnitType = unitType };
                    foreach(long monsterId in message.UnitIds)
                    {
                        UnitInfo unitInfo = new UnitInfo();
                        unitInfo.UnitId = monsterId;
                        m2C_RemoveUnits1.Units.Add(unitInfo);

                    }
                    MessageHelper.Broadcast(m2C_RemoveUnits1, message.PlayerUnitIds.ToArray());

                    Console.WriteLine(" M2M_RemoveUnitHandler-25-sessionId/unitId/unittype： " + session.Id + " / " + message.UnitIds.First() + " / " + (UnitType)message.UnitType);
                    break;

            }

        }   

    }
}
