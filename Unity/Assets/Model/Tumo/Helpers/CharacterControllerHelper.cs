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
            self.animator = unit.GetComponent<TmAnimatorComponent>();
            if (self.animator.animator == null || self.controller == null)
            {
                self.animator.animator = unit.GameObject.GetComponent<Animator>();
                self.controller = unit.GameObject.GetComponent<CharacterController>();
            }

            if (self.isCanControl)
            {
                self.h = Input.GetAxis("Horizontal");    //获取水平方线   //默认 Horizontal a键 为 -1  d键为 1 
                self.v = Input.GetAxis("Vertical");      //获取水平方线    //默认 Vertical s键 为 -1  w键为 1   

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

            if (IsGrounded(unit.GameObject))
            {
                unit.GameObject.GetComponent<Transform>().Rotate(0, self.h * self.roteSpeed, 0);
                self.moveDirection = new Vector3(0, 0, self.v);
                self.moveDirection = unit.GameObject.GetComponent<Transform>().TransformDirection(self.moveDirection);
                self.moveDirection *= self.moveSpeed;
                if (Input.GetButton("Jump"))
                {
                    self.moveDirection.y = self.jumpSpeed;
                }
            }

            self.moveDirection.y -= self.gravity * Time.deltaTime;
            self.controller.Move(self.moveDirection * Time.deltaTime);

            self.animator.AnimSet(self.v);

            //Debug.Log(" CharacterControllerHelper-58-v/h: " + TimeHelper.ClientNow() + " : " +  self.v + " / " + self.h);

            self.SetMapMove();

        }

        static bool IsGrounded(GameObject go)
        {
            return Physics.Raycast(go.GetComponent<Transform>().position, -Vector3.up, 0.2f);
        }

        static void SetMapMove(this CharacterControllerComponent self)
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

            //self.v = Input.GetAxis("Vertical") * self.moveSpeed;
            //self.h = Input.GetAxis("Horizontal") * self.roteSpeed;

            if (Math.Abs(self.v) > 0.05f || Math.Abs(self.h) > 0.05f)
            {
                if (self.startTime == 0)
                {
                    self.move_Map.KeyType = (int)KeyType.KeyCode;
                    self.move_Map.Id = ETModel.Game.Scene.GetComponent<PlayerComponent>().MyPlayer.UnitId;

                    self.move_Map.V = self.v;
                    self.move_Map.H = self.h;

                    ETModel.SessionComponent.Instance.Session.Send(self.move_Map);

                    self.isStart = true;

                    //Debug.Log(" CharacterControllerHelper-91: " + TimeHelper.ClientNow() + " : " + (KeyType)self.move_Map.KeyType + " / " + self.move_Map.Id + " / " + self.move_Map.V + " / " + self.move_Map.H);
                }
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
                        self.move_Map.KeyType = (int)KeyType.Click;
                        self.move_Map.X = self.ClickPoint.x;
                        self.move_Map.Y = self.ClickPoint.y;
                        self.move_Map.Z = self.ClickPoint.z;
                        ETModel.SessionComponent.Instance.Session.Send(self.move_Map);

                        Debug.Log(" CharacterControllerHelper-116: " + (KeyType)self.move_Map.KeyType + " / " + self.move_Map.Id + " / " + self.move_Map.X + " / " + self.move_Map.Y + " / " + self.move_Map.Z);
                    }
                }
            }
        }



    }
}
