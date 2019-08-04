using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETHotfix
{
    public static class KeyboardPathComponentHelper
    {
        public static void FramevhToMapServer(this KeyboardPathComponent self)
        {
            self.v = Input.GetAxis("Vertical");      //获取水平方线    //默认 Vertical s键 为 -1  w键为 1          
            self.h = Input.GetAxis("Horizontal");    //获取水平方线   //默认 Horizontal a键 为 -1  d键为 1 

            if (Math.Abs(self.v) > 0.05f || Math.Abs(self.h) > 0.05f)
            {
                self.isZero = false;
                long playerUnitId = ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.UnitId;
                Unit player = ETModel.Game.Scene.GetComponent<UnitComponent>().Get(playerUnitId);
                self.w = player.Rotation.eulerAngles.y;

                self.c2M_PathKeyboardResult.KeyType = (int)KeyType.KeyCode;
                self.c2M_PathKeyboardResult.Id = playerUnitId;
                self.c2M_PathKeyboardResult.V = self.v;
                self.c2M_PathKeyboardResult.H = self.h;
                self.c2M_PathKeyboardResult.W = self.w;

                ETModel.SessionComponent.Instance.Session.Send(self.c2M_PathKeyboardResult);

                Debug.Log(" KeyboardPathComponentUpdateSystem-47: " + (KeyType)self.c2M_PathKeyboardResult.KeyType + " / " + self.c2M_PathKeyboardResult.Id + " :( " + self.c2M_PathKeyboardResult.V + " / " + self.c2M_PathKeyboardResult.H + ")");
            }
            else
            {
                if (!self.isZero)
                {
                    self.v = 0;
                    self.h = 0;
                    self.isZero = true;

                    self.c2M_PathKeyboardResult.KeyType = (int)KeyType.KeyCode;
                    self.c2M_PathKeyboardResult.Id = ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.UnitId;
                    self.c2M_PathKeyboardResult.V = self.v;
                    self.c2M_PathKeyboardResult.H = self.h;

                    ETModel.SessionComponent.Instance.Session.Send(self.c2M_PathKeyboardResult);

                    Debug.Log(" KeyboardPathComponentUpdateSystem-47: " + (KeyType)self.c2M_PathKeyboardResult.KeyType + " / " + self.c2M_PathKeyboardResult.Id + " :( " + self.c2M_PathKeyboardResult.V + " / " + self.c2M_PathKeyboardResult.H + ")");
                }
            }
        }



    }
}
