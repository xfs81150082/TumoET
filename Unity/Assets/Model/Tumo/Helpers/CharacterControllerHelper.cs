using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
    public static class CharacterControllerHelper
    {
        public static void KeyMove(this CharacterControllerComponent self)
        {
            Unit unit = self.GetParent<Unit>();
            if (self.animatorComponent == null)
            {
                self.animatorComponent = unit.GetComponent<AnimatorComponent>();
            }

            if (self.isCanControl)
            {
                self.h = Input.GetAxis("Horizontal");    //获取水平方线   //默认 Horizontal a键 为 -1  d键为 1 
                self.v = Input.GetAxis("Vertical");      //获取水平方线   //默认 Vertical s键 为 -1  w键为 1   

                //if (Mathf.Abs(EngineerJoyStick.hv2.x) > 10.0f || Mathf.Abs(EngineerJoyStick.hv2.y) > 10.0f)
                //{
                //    self.h = EngineerJoyStick.hv2.x / EngineerJoyStick.mRadius;
                //    self.v = EngineerJoyStick.hv2.y / EngineerJoyStick.mRadius;
                //}
                //else
                //{
                //    self.h = Input.GetAxis("Horizontal");    //获取水平方线   //默认 Horizontal a键 为 -1  d键为 1 
                //    self.v = Input.GetAxis("Vertical");      //获取水平方线    //默认 Vertical s键 为 -1  w键为 1   
                //    //Debug.Log(" move.v: " + move.v );
                //}
            }

            if (self.IsGrounded())
            {
                self.moveDirection = new Vector3(0, 0, self.v);
                self.moveDirection = unit.GameObject.transform.TransformDirection(self.moveDirection);
                self.moveDirection *= self.moveSpeed;
                if (Input.GetButton("Jump"))
                {
                    self.moveDirection.y = self.jumpSpeed;
                }
            }

            self.moveDirection.y -= self.gravity * Time.deltaTime;
            self.Controller.Move(self.moveDirection * Time.deltaTime);
            unit.GameObject.transform.Rotate(new Vector3(0, self.h * self.roteSpeed, 0));

            self.animatorComponent.AnimSet(self.v);

            self.SetMoveMap();
        }

        public static bool IsGrounded(this CharacterControllerComponent self)
        {
            return Physics.Raycast(self.GetParent<Unit>().GameObject.transform.position, -Vector3.up, self.gdy);
        }

        static void SetMoveMap(this CharacterControllerComponent self)
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

            if (Math.Abs(self.h) > 0.05f)
            {
                self.resTime = 0.3f;
            }
            else
            {
                if (self.resTime != 1.0f)
                {
                    self.resTime = 1.0f;
                }
            }

            if (Math.Abs(self.v) > 0.05f)
            {
                if (self.startTime == 0)
                {
                    Vector3 clientPos = self.GetParent<Unit>().Position;
                    Vector3 dir = self.moveDirection;

                    self.move_Map.KeyType = (int)KeyType.KeyCode;
                    self.move_Map.Id = ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.UnitId;

                    self.move_Map.V = self.v;

                    self.move_Map.X = clientPos.x;
                    self.move_Map.Y = 0;
                    self.move_Map.Z = clientPos.z;

                    self.move_Map.AX = dir.x;
                    self.move_Map.AY = 0;
                    self.move_Map.AZ = dir.z;

                    ETModel.SessionComponent.Instance.Session.Send(self.move_Map);

                    self.isStart = true;
                    self.isZero = false;
                }
            }
            else
            {
                if (!self.isZero)
                {
                    self.v = 0;
                    self.moveDirection = Vector3.zero;

                    Vector3 clientPos = self.GetParent<Unit>().Position;
                    Vector3 dir = self.moveDirection;

                    self.move_Map.KeyType = (int)KeyType.KeyCode;
                    self.move_Map.Id = ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.UnitId;

                    self.move_Map.V = self.v;

                    self.move_Map.X = clientPos.x;
                    self.move_Map.Y = 0;
                    self.move_Map.Z = clientPos.z;

                    self.move_Map.AX = dir.x;
                    self.move_Map.AY = 0;
                    self.move_Map.AZ = dir.z;

                    ETModel.SessionComponent.Instance.Session.Send(self.move_Map);

                    self.isZero = true;

                    Debug.Log(" CharacterControllerHelper-146-clientPos/dir: " + clientPos.ToString() + " / " + dir.ToString());
                }
            }

        }



    }
}
