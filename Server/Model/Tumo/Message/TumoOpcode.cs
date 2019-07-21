using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [Message(TumoOpcode.W2M_DeathActorRequest)]
    public partial class W2M_DeathActorRequest : IActorLocationRequest { }

    [Message(TumoOpcode.M2W_DeathActorResponse)]
    public partial class M2W_DeathActorResponse : IActorLocationResponse { }

    [Message(TumoOpcode.KeyCode_TranslateMap)]
    public partial class KeyCode_TranslateMap : IActorLocationMessage { }





}
namespace ETModel
{

    public static partial class TumoOpcode
    {
        public const ushort W2M_DeathActorRequest = 41011;
        public const ushort M2W_DeathActorResponse = 41012;

        public const ushort KeyCode_TranslateMap = 41021;


    }

}
