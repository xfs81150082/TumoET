using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
    public static class TranslateComponentHelper
    {
        public static void KeyCodeContorlMove(this TranslateComponent self)
        {
            self.Init();                               /// self 初始化

            //self.ContorlMove();                        /// 控制 向角色正前方移动和角色原地旋转

            //self.ControlJump();                        /// 控制 跳跃

            //self.animator.AnimSet(self.v);             /// 控制 动画

            self.SetMapMove();                         /// 控制 状态同步
        }

        /// self 初始化
        static void Init(this TranslateComponent self)
        {
            if (self.roler == null)
            {
                self.roler = self.GetParent<Unit>().GameObject;
            }
            if (self.animator == null)
            {
                self.animator = self.GetParent<Unit>().GetComponent<TmAnimatorComponent>();
            }
            if (self.animator.animator == null)
            {
                self.animator.animator = self.GetParent<Unit>().GameObject.GetComponent<Animator>();
            }
        }

        /// 控制 向角色正前方移动和角色原地旋转
        static void ContorlMove(this TranslateComponent self)
        {
            if (self.isCanControl)
            {
                self.h = Input.GetAxis("Horizontal");    //获取水平方线   //默认 Horizontal a键 为 -1  d键为 1 
                self.v = Input.GetAxis("Vertical");      //获取水平方线    //默认 Vertical s键 为 -1  w键为 1   

                #region
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
                #endregion
            }

            self.GetParent<Unit>().GameObject.GetComponent<Transform>().Translate(0, 0, self.v * self.moveSpeed * Time.deltaTime);

            if (self.IsGrounded())
            {
                self.GetParent<Unit>().GameObject.GetComponent<Transform>().Rotate(0, self.h * self.roteSpeed, 0);
            }
        }

        /// 控制 跳跃
        static void ControlJump(this TranslateComponent self)
        {
            if (Input.GetButton("Jump") && self.IsGrounded())//如果点击了触发跳跃的键盘按钮的话
            {
                //每点击一次，上升。 上升的中途过程中，如果再次点击，就再次上升。上升到某个临界值，开始下降，下降到地面，就取消Jump。停止Jump运算 
                if (self.IsJumpUp && self.IsJump)
                {
                    //刷新起始跳跃速度  
                    self.actuallySpeed = self.jumpSpeed;//每点一次跳跃都要设置初始速度单位
                    return;
                }
                if (self.IsJumpDown && self.IsJump)
                {
                    //刷新跳跃速度
                    self.actuallySpeed = self.jumpSpeed;//每点一次跳跃都要设置初始速度单位
                    self.IsJumpUp = true;
                    self.IsJumpDown = false;
                    return;
                }

                self.actuallySpeed = self.jumpSpeed;//每点一次跳跃都要设置初始速度单位
                self.IsJump = true;
                self.IsJumpUp = true;
            }

            if (self.IsJump)
            {
                if (self.IsJumpUp)      //还在上升期
                {
                    self.GetParent<Unit>().GameObject.transform.Translate(new Vector3(0, self.actuallySpeed * Time.deltaTime, 0));
                    self.actuallySpeed -= self.gravity;

                    if (self.actuallySpeed <= 0)
                    {
                        Debug.Log("switch");
                        self.IsJumpDown = true;
                        self.IsJumpUp = false;
                    }
                }

                if (self.IsJumpDown)       //朝下
                {
                    self.GetParent<Unit>().GameObject.transform.Translate(new Vector3(0, -self.actuallySpeed * Time.deltaTime, 0));
                    self.actuallySpeed += self.gravity;            //加速下落 Position的Y 轴一直在减少

                    if (self.GetParent<Unit>().GameObject.transform.position.y <= self.dy)
                    {
                        Vector3 currentPosition = self.GetParent<Unit>().GameObject.transform.position;
                        self.GetParent<Unit>().GameObject.transform.position = new Vector3(currentPosition.x, 0, currentPosition.z);
                        self.IsJumpDown = false;
                        self.IsJump = false;
                        self.actuallySpeed = self.jumpSpeed;
                    }
                }

                Debug.Log(" TranslateComponentHelper-229: " + TimeHelper.ClientNow() + " Jump: " + self.GetParent<Unit>().Position);
            }
        }

        /// 角色离地0.2米以内 返回真 否则假
        static bool IsGrounded(this TranslateComponent self)
        {
            if (self.GetParent<Unit>().Position.y < self.dy)
            {
                return true;
            }
            return false;
        }

        static bool IsGrounded(GameObject go)
        {
            return Physics.Raycast(go.GetComponent<Transform>().position, -Vector3.up, 0.2f);
        }

        public static void SetMapMove(this TranslateComponent self)
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

                    Debug.Log(" TranslateComponentHelper-178: " + (KeyType)self.c2M_PathKeyboardResult.KeyType + " / " + self.c2M_PathKeyboardResult.Id + " :( " + self.c2M_PathKeyboardResult.V + " / " + self.c2M_PathKeyboardResult.H + ")");
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

                    Debug.Log(" TranslateComponentHelper-178: " + (KeyType)self.c2M_PathKeyboardResult.KeyType + " / " + self.c2M_PathKeyboardResult.Id + " :( " + self.c2M_PathKeyboardResult.V + " / " + self.c2M_PathKeyboardResult.H + ")");
                }


                /////点击移动
                //if (Input.GetMouseButtonDown(1))
                //{
                //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //    RaycastHit hit;
                //    if (Physics.Raycast(ray, out hit, 1000, self.mapMask))
                //    {
                //        self.ClickPoint = hit.point;
                //        self.c2M_PathKeyboardResult.KeyType = (int)KeyType.Click;
                //        self.c2M_PathKeyboardResult.X = self.ClickPoint.x;
                //        self.c2M_PathKeyboardResult.Y = self.ClickPoint.y;
                //        self.c2M_PathKeyboardResult.Z = self.ClickPoint.z;
                //        ETModel.SessionComponent.Instance.Session.Send(self.c2M_PathKeyboardResult);

                //        Debug.Log(" TranslateComponentHelper-197: " + (KeyType)self.c2M_PathKeyboardResult.KeyType + " / " + self.c2M_PathKeyboardResult.Id + " :( " + self.c2M_PathKeyboardResult.X + ", " + self.c2M_PathKeyboardResult.Y + ", " + self.c2M_PathKeyboardResult.Z + ")");
                //    }
                //}
            }
        }



    }
}
