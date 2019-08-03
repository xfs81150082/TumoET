using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [ActorMessageHandler(AppType.Map)]
    public class C2M_KeyboardPathHandler : AMActorLocationHandler<Unit, C2M_KeyboardPathResult>
    {
        protected override void Run(Unit entity, C2M_KeyboardPathResult message)
        {
            entity.GetComponent<KeyboardPathComponent>().KeyboardVW(message.V, message.W);
        }


    }
}
