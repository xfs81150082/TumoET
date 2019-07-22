using System.Collections;
using System.Collections.Generic;
using ETModel;
using UnityEngine;

namespace ETHotfix
{
    [MessageHandler]
    public class M2C_MoveKeyCodeHandler : AMHandler<Move_KeyCodeMap>
    {
        protected override void Run(ETModel.Session session, Move_KeyCodeMap message)
        {
            Debug.Log(" M2C_MoveKeyCodeHandler-12: " + TimeHelper.ClientNow());

        }

    }


}
