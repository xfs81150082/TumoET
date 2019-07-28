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
        public static void SetMapMove(this KeyboardPathComponent self)
        {
            if (self.isStart)
            {
                self.startTime += Time.deltaTime;
            }
            if (self.startTime > self.resTime)
            {
                self.startTime = 0;
                self.isStart = false;
            }

            self.v = Input.GetAxis("Vertical");      //获取水平方线    //默认 Vertical s键 为 -1  w键为 1          
            self.h = Input.GetAxis("Horizontal");    //获取水平方线   //默认 Horizontal a键 为 -1  d键为 1 

            if (Math.Abs(self.v) > 0.05f || Math.Abs(self.h) > 0.05f)
            {
                if (self.startTime == 0)
                {
                    self.c2M_PathKeyboardResult.KeyType = (int)KeyType.KeyCode;
                    self.c2M_PathKeyboardResult.Id = ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.UnitId;
                    self.c2M_PathKeyboardResult.V = self.v;
                    self.c2M_PathKeyboardResult.H = self.h;

                    ETModel.SessionComponent.Instance.Session.Send(self.c2M_PathKeyboardResult);

                    self.isStart = true;

                    Debug.Log(" KeyboardPathComponentUpdateSystem-47: " + (KeyType)self.c2M_PathKeyboardResult.KeyType + " / " + self.c2M_PathKeyboardResult.Id + " :( " + self.c2M_PathKeyboardResult.V + " / " + self.c2M_PathKeyboardResult.H + ")");
                }
                if (self.isZero)
                {
                    self.isZero = false;
                }
            }
            else
            {
                if (!self.isStart)
                {
                    self.isStart = true;
                }

                if (!self.isZero)
                {
                    self.c2M_PathKeyboardResult.KeyType = (int)KeyType.KeyCode;
                    self.c2M_PathKeyboardResult.Id = ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.UnitId;
                    self.c2M_PathKeyboardResult.V = 0;
                    self.c2M_PathKeyboardResult.H = 0;

                    ETModel.SessionComponent.Instance.Session.Send(self.c2M_PathKeyboardResult);

                    self.isZero = true;

                    Debug.Log(" KeyboardPathComponentUpdateSystem-72: " + (KeyType)self.c2M_PathKeyboardResult.KeyType + " / " + self.c2M_PathKeyboardResult.Id + " :( " + self.c2M_PathKeyboardResult.V + " / " + self.c2M_PathKeyboardResult.H + ")");
                }

                ///点击移动
                if (Input.GetMouseButtonDown(1))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
                    {
                        self.ClickPoint = hit.point;
                        self.c2M_PathKeyboardResult.KeyType = (int)KeyType.Click;
                        self.c2M_PathKeyboardResult.X = self.ClickPoint.x;
                        self.c2M_PathKeyboardResult.Y = self.ClickPoint.y;
                        self.c2M_PathKeyboardResult.Z = self.ClickPoint.z;
                        ETModel.SessionComponent.Instance.Session.Send(self.c2M_PathKeyboardResult);

                        Debug.Log(" KeyboardPathComponentUpdateSystem-91: " + (KeyType)self.c2M_PathKeyboardResult.KeyType + " / " + self.c2M_PathKeyboardResult.Id + " :( " + self.c2M_PathKeyboardResult.X + ", " + self.c2M_PathKeyboardResult.Y + ", " + self.c2M_PathKeyboardResult.Z + ")");
                    }
                }
            }
        }



    }
}
