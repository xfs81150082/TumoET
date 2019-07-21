using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [Message(TumoOpcode.W2M_DeathActorRequest)]
    public partial class W2M_DeathActorRequest : IActorLocationRequest { }

    [Message(TumoOpcode.M2W_DeathActorResponse)]
    public partial class M2W_DeathActorResponse : IActorLocationResponse { }

    [Message(TumoOpcode.Move_Map)]
    public partial class Move_Map : IActorLocationMessage { }

    [Message(TumoOpcode.Frame_KeyCodeMap)]
    public partial class Frame_KeyCodeMap : IActorLocationMessage { }




}
namespace ETModel
{

    public static partial class TumoOpcode
    {
        public const ushort W2M_DeathActorRequest = 41011;
        public const ushort M2W_DeathActorResponse = 41012;

        public const ushort Move_Map = 41021;
        public const ushort Frame_KeyCodeMap = 41022;




    }

}
