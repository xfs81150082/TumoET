using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [MessageHandler(AppType.Map)]
    public class M2M_RemoveUnitHandler : AMHandler<M2M_RemoveUnit>
    {
        protected override void Run(Session session, M2M_RemoveUnit message)
        { 
            Console.WriteLine(" 死亡结算-sessionId/unitId/unittype： " + session.Id + " / " + message.UnitId + " / " + (UnitType)message.UnitType);
            //1、删除 客户端Unit实例或其GameObject
            //2、将服务端Unit 处理一下，如标高为死亡状态，或移出组件管理的字典


            switch ((UnitType)message.UnitType)
            {
                case UnitType.Player:
                    break;
                case UnitType.Monster:
                    MessageHelper.Broadcast(new M2C_RemoveUnit() { Request = message.UnitId.ToString(), Message = message.UnitType.ToString() });

                    Console.WriteLine(" M2M_RemoveUnitHandler-25-sessionId/unitId/unittype： " + session.Id + " / " + message.UnitId + " / " + (UnitType)message.UnitType);
                    break;

            }

        }   

    }
}
