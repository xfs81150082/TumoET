using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static class SeeComponentHelper
    {
        /// <summary>
        /// 发送追击目标坐标 消息
        /// </summary>
        /// <param name="self"></param>
        public static void SendSeePosition(this SeeComponent self)
        {
                
            // 发送一次目标点坐标 消息
            self.seeMap = self.GetSeeMap();
            if (self.seeMap != null)
            {
                ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get((self.Parent as Unit).Id);
                actorLocationSender.Send(self.seeMap);
            }
        }

        /// 追击敌人
        static See_Map GetSeeMap (this SeeComponent self)
        {
            if(self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastUnit == null)
            {
                return null;
            }
            self.target = self.GetParent<Unit>().GetComponent<SqrDistanceComponent>().neastUnit;
            self.seePoint = self.target.Position;
            See_Map see_Map = new See_Map() { Id = self.GetParent<Unit>().Id, X = self.seePoint.x, Y = self.seePoint.y, Z = self.seePoint.z };
            return see_Map;
        }
        

    }
}