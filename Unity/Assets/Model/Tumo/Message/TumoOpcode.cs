using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [Message(TumoOpcode.W2M_DeathActorRequest)]
    public partial class W2M_DeathActorRequest : IActorLocationRequest { }

    [Message(TumoOpcode.M2W_DeathActorResponse)]
    public partial class M2W_DeathActorResponse : IActorLocationResponse { }





}
namespace ETModel
{

    public static partial class TumoOpcode
    {
        public const ushort W2M_DeathActorRequest = 41011;
        public const ushort M2W_DeathActorResponse = 41012;

    }

}
