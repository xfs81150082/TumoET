using ETModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ETHotfix
{
    public static class SessionHelper
    {
        public static Session MapSession()
        {
            /// 得到 Map 服务器 Session
            IPEndPoint mapAddress = StartConfigComponent.Instance.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
            return Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
        }

    }
}
