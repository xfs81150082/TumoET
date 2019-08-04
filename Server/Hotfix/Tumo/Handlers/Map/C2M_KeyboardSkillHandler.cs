using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class C2M_KeyboardSkillHandler : AMActorLocationHandler<Unit, C2M_KeyboardSkillResult>
    {
        protected override void Run(Unit entity, C2M_KeyboardSkillResult message)
        {

            Console.WriteLine(" C2M_KeyboardSkillHandler-13:  " + message.Keyboard);

        }


    }
}
