using System;
using System.Collections.Generic;
using System.Text;
using ETModel;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class Move_MapHandler : AMActorLocationHandler<Unit, KeyCode_TranslateMap>
    {
        protected override void Run(Unit player, KeyCode_TranslateMap message)
        {
            Console.WriteLine(" KeyType_WSADMapHandler: " + message.KeyType + " / " + player.Id + " / " + message.Id + " / " + message.WS + " / " + message.AD);
        }

        void UnitMove(Unit unit, KeyCode_TranslateMap message)
        {

        }
        void UnitTrun(Unit unit, KeyCode_TranslateMap message)
        {

        }


    }


}
