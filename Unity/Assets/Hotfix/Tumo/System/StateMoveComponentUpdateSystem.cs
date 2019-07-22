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
    public class StateMoveComponentUpdateSystem : UpdateSystem<StateMoveComponent>
    {
        public override void Update(StateMoveComponent self)
        {
            TranslateMove(self);
        }

        private readonly Move_Map move_Map = new Move_Map();

        void TranslateMove(StateMoveComponent self)
        {
            self.wsForword = Input.GetAxis("Vertical") * self.moveSpeed;
            self.adTrun = Input.GetAxis("Horizontal") * self.roteSpeed;

            if (Math.Abs(self.wsForword) > 0.05f || Math.Abs(self.adTrun) > 0.05f)
            {
                move_Map.KeyType = (int)KeyType.KeyCode;
                move_Map.Id = ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.UnitId;

                move_Map.WS = self.wsForword;
                move_Map.AD = self.adTrun;

                ETModel.SessionComponent.Instance.Session.Send(move_Map);

                //Debug.Log(" StateMoveComponentUpdateSystem-36: " + (KeyType)move_Map.KeyType + " / " + move_Map.Id + " / " + self.wsForword + " / " + self.adTrun);
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
                        move_Map.KeyType = (int)KeyType.Click;
                        move_Map.X = self.ClickPoint.x;
                        move_Map.Y = self.ClickPoint.y;
                        move_Map.Z = self.ClickPoint.z;
                        ETModel.SessionComponent.Instance.Session.Send(move_Map);

                        Debug.Log(" StateMoveComponentUpdateSystem-53: " + (KeyType)move_Map.KeyType + " / " + move_Map.Id + " / " + move_Map.X + " / " + move_Map.Y + " / " + move_Map.Z);
                    }
                }
            }
        }


    }

}
