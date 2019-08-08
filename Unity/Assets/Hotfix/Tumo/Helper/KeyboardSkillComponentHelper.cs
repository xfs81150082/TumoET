using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETHotfix
{
    public static class KeyboardSkillComponentHelper
    {
        public static void SkillKeyboardToMapServer(this KeyboardSkillComponent self)
        {
            self.UpdateKey();
        }

        public static void UpdateKey(this KeyboardSkillComponent self)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                self.currentKey = KeyCode.R;
                self.isSend = true;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                self.currentKey = KeyCode.E;
                self.isSend = true;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                self.currentKey = KeyCode.Q;
                self.isSend = true;
            }
            self.KeySend().Coroutine();
        }

        static async ETVoid KeySend(this KeyboardSkillComponent self)
        {
            if (self.isSend)
            {
                self.c2M_KeyboardSkillRequest.Info = self.currentKey.ToString();
                M2C_KeyboardSkillResponse response = (M2C_KeyboardSkillResponse)await ETModel.SessionComponent.Instance.Session.Call(self.c2M_KeyboardSkillRequest);
                self.m2C_KeyboardSkillResponse.Info = response.Info;
                self.m2C_KeyboardSkillResponse.Message = response.Message;
                self.isSend = false;

                Debug.Log(" KeyboardSkillComponentHelper-17: " + self.c2M_KeyboardSkillRequest.Info);
            }
        }


    }
}
