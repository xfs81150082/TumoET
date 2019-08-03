using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class C2M_MoveMapHandler : AMActorLocationHandler<Unit, Move_Map>
    {
        protected override void Run(Unit entity, Move_Map message)
        {
            //Console.WriteLine(" C2M_MoveMapHandler-13-v/dir:  " + message.V + " ( " + message.AX + ", " + message.AY + ", " + message.AZ + ")");

            entity.GetComponent<MapPathComponent>().MoveMap(message);
        }
    }
}
