using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Move_MapHandler : AMActorLocationHandler<Unit, Frame_KeyCodeMap>
    {
        protected override void Run(Unit player, Frame_KeyCodeMap message)
        {
            Console.WriteLine(" KeyType_WSADMapHandler: " + message.KeyType + " / " + player.Id + " / " + message.Id + " / " + message.WS + " / " + message.AD);
        }

        void UnitMove(Unit unit, Frame_KeyCodeMap message)
        {

        }
        void UnitTrun(Unit unit, Frame_KeyCodeMap message)
        {

        }


    }


}
