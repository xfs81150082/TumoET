using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static class ActorHelper
    {
        public static ActorLocationSender ActorLocation(long unitId)
        {
            /// 得到 ActorLocat 
            ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(unitId);
            return actorLocationSender;
        }

        public static ActorLocationSender Actor(long unitId)
        {
            /// 得到 ActorLocat 
            ActorLocationSender actorLocationSender = Game.Scene.GetComponent<ActorLocationSenderComponent>().Get(unitId);
            return actorLocationSender;
        }

    }
}
