using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class ServerMoveComponentUpdateSystem : UpdateSystem<ServerMoveComponent>
    {
        public override void Update(ServerMoveComponent self)
        {
            self.Update();
        }
    }

    public class ServerMoveComponent : Component
    {
        public bool isCanControl = true;                   //表示是否可以用键盘控制
        public float h = 0;
        public float v = 0;
        public float moveSpeed = 4.0f;                      //移动速度
        public float roteSpeed = 5.0f;                      //旋转速度
        public float resTime = 100f;

        public bool isStart1 = false;
        public long startTime1 = 0;
        public long offsetTime1 = 0;

        public bool isStart2 = false;
        public long startTime2 = 0;
        public long offsetTime2 = 0;

        public void Update()
        {
            Move();
            Trun();
        }

        void Move()
        {
            if (Math.Abs(v) > 0.05f)
            {
                if (!isStart1)
                {
                    startTime1 = TimeHelper.Now();
                    isStart1 = true;
                }

                offsetTime1 = TimeHelper.Now() - startTime1;

                if (offsetTime1 > resTime)
                {
                    float dx = (float)Math.Cos(this.GetParent<Unit>().eulerAngles.y) * v * moveSpeed * resTime / 1000;
                    float dz = (float)Math.Sin(this.GetParent<Unit>().eulerAngles.y) * v * moveSpeed * resTime / 1000;

                    float px = this.GetParent<Unit>().Position.x + dx;
                    float pz = this.GetParent<Unit>().Position.z + dz;
                    float mapWide = Game.Scene.GetComponent<AoiGridComponent>().mapWide;

                    if (px > mapWide/2)
                    {
                        px = mapWide / 2;
                    }
                    if (px < -mapWide / 2)
                    {
                        px = -mapWide / 2;
                    }
                    if (pz > mapWide / 2)
                    {
                        pz = mapWide / 2;
                    }
                    if (pz < -mapWide / 2)
                    {
                        pz = -mapWide / 2;
                    }

                    this.GetParent<Unit>().Position = new Vector3(px, 0, pz);

                    isStart1 = false;

                    Console.WriteLine(" ServerMoveComponent-82-px/pz: " + this.GetParent<Unit>().Id + " : ( " + dx + " , " + dz + ")" + " / ( " + this.GetParent<Unit>().Position.x + " , " + 0 + " , " + this.GetParent<Unit>().Position.z + ")");
                }

            }
            else
            {
                if (isStart1)
                {
                    isStart1 = false;

                    Console.WriteLine(" ServerMoveComponent-92: " + isStart1 + " / " + startTime1);
                }
            }
        }

        void Trun()
        {
            if (Math.Abs(h) > 0.05f)
            {
                if (!isStart2)
                {
                    startTime2 = TimeHelper.Now();
                    isStart2 = true;
                }

                offsetTime2 = TimeHelper.Now() - startTime2;

                if (offsetTime2 > resTime)
                {
                    float ay = this.GetParent<Unit>().eulerAngles.y + h * roteSpeed;

                    if (ay > 360)
                    {
                        ay -= 360;
                    }
                    if (ay < 0)
                    {
                        ay += 360;
                    }

                    this.GetParent<Unit>().eulerAngles = new Vector3(0, ay, 0);                 

                    isStart2 = false;

                    Console.WriteLine(" ServerMoveComponent-126: " + this.GetParent<Unit>().UnitType + " / " + this.GetParent<Unit>().Id + " : ( " + 0 + " , " + ay + " , " + 0 + ")");
                }

            }
            else
            {
                if (isStart2)
                {
                    isStart2 = false;

                    Console.WriteLine(" ServerMoveComponent-136: " + isStart2 + " / " + startTime2);
                }
            }

        }
        

    }
}
