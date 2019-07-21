using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class TranslateMoveComponentUpdateSystem : UpdateSystem<TranslateMoveComponent>
    {
        public override void Update(TranslateMoveComponent self)
        {
            TranslateMove(self);
        }
        
        private readonly Frame_ClickMap frameClickMap = new Frame_ClickMap();
        
        private readonly KeyCode_TranslateMap keyCode_TranslateMap = new KeyCode_TranslateMap();

        void TranslateMove(TranslateMoveComponent self)
        {
            self.wsForword = Input.GetAxis("Vertical") * self.moveSpeed;
            self.adTrun = Input.GetAxis("Horizontal") * self.roteSpeed;

            if (Math.Abs(self.wsForword) > 0.05f || Math.Abs(self.adTrun) > 0.05f)
            {
                keyCode_TranslateMap.KeyType = (int)KeyType.KeyCode;
                keyCode_TranslateMap.Id = ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.UnitId;

                keyCode_TranslateMap.WS = self.wsForword;
                keyCode_TranslateMap.AD = self.adTrun;

                Debug.Log(" unitid/wsForword/adTrun-32: " + ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.UnitId + " / " + self.wsForword + " / " + self.adTrun);

                ETModel.SessionComponent.Instance.Session.Send(keyCode_TranslateMap);
            }
            else
            {
                if (Input.GetMouseButtonDown(1))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
                    {
                        self.ClickPoint = hit.point;
                        frameClickMap.X = self.ClickPoint.x;
                        frameClickMap.Y = self.ClickPoint.y;
                        frameClickMap.Z = self.ClickPoint.z;
                        ETModel.SessionComponent.Instance.Session.Send(frameClickMap);
                    }
                }
            }
        }


    }




}
