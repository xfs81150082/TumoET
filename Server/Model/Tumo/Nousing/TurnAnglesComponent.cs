using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class TurnComponentUpdateSystem : UpdateSystem<TurnAnglesComponent>
    {
        public override void Update(TurnAnglesComponent self)
        {
            self.Update();
        }
    }

    public class TurnAnglesComponent : Component
    {
        // turn
        public Vector3 TargetEulerAngles;

        // 开启移动协程的时间
        public long StartTime;

        // 开启移动协程的Unit的位置
        public Vector3 StartEul;

        public long needTime;
  
        // 当前的旋转速度
        public float ratateSpeed = 14.0f;

        public bool isSky = true;

        /// <summary>
        /// 异步 移动到 目标点
        /// </summary>
        /// <param name="target"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ETTask TurnToAsync(Vector3 targetEulerAngles, CancellationToken cancellationToken)
        {
            isSky = false;

            // 新目标点离旧目标点太近，不设置新的
            if (Math.Abs(targetEulerAngles.y - this.TargetEulerAngles.y) < 0.1f)
            {
                return;
            }

            // 距离当前位置太近
            if (Math.Abs(this.GetParent<Unit>().EulerAngles.y - targetEulerAngles.y) < 0.1f)
            {
                return;
            }

            this.TargetEulerAngles = targetEulerAngles;

            //Console.WriteLine(" TurnAnglesComponent-47-tx/tz: " + " ( " + 0 + " , " + targetEulerAngles.y + " , " + 0 + ")");

            // 开启协程移动
            await StartTurn(cancellationToken);

        }

        // 开启协程移动,每100毫秒移动一次，并且协程取消的时候会计算玩家真实移动
        // 比方说玩家移动了250毫秒,玩家有新的目标,这时旧的移动协程结束,将计算250毫秒移动的位置，而不是300毫秒移动的位置
        public async ETTask StartTurn(CancellationToken cancellationToken)
        {
            Unit unit = this.GetParent<Unit>();
            this.StartEul = unit.EulerAngles;
            this.StartTime = TimeHelper.Now();
            float angle = Math.Abs(this.TargetEulerAngles.y - this.StartEul.y);

            Vector3 target = new Vector3(0, this.TargetEulerAngles.y,0);

            if (angle > 180.0f)
            {                
                angle = 360 - angle;               
            }

            //Console.WriteLine(" TurnAnglesComponent-65-distance: " + angle);

            if (angle < 0.1f)
            {
                return;
            }

            this.needTime = (long)(angle / this.ratateSpeed * 1000);

            //Console.WriteLine(" TurnAnglesComponent-74-needTime: " + this.needTime);

            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();

            // 协程如果取消，将算出玩家的真实位置，赋值给玩家
            cancellationToken.Register(() =>
            {
                long timeNow = TimeHelper.Now();
                if (timeNow - this.StartTime >= this.needTime)
                {
                    unit.EulerAngles = this.TargetEulerAngles;
                }
                else
                {
                    float amount = (timeNow - this.StartTime) * 1f / this.needTime;
                    unit.EulerAngles = Vector3.Lerp(this.StartEul, this.TargetEulerAngles, amount);
                }

                isSky = true;

            });

            while (true)
            {
                await timerComponent.WaitAsync(50, cancellationToken); 

                long timeNow = TimeHelper.Now();

                if (timeNow - this.StartTime >= this.needTime)
                {
                    unit.EulerAngles = this.TargetEulerAngles;
                    break;
                }

                float amount = (timeNow - this.StartTime) * 1f / this.needTime;
                unit.EulerAngles = Vector3.Lerp(this.StartEul, this.TargetEulerAngles, amount);

                Console.WriteLine(" TurnAnglesComponent-129-targetH: " + unit.UnitType + " / ( " + 0 + " , " + target.y + " , " + 0 + ")");
                Console.WriteLine(" TurnAnglesComponent-130-unitH: " + unit.UnitType + " / ( " + 0 + " , " + unit.EulerAngles.y + " , " + 0 + ")");
                Console.WriteLine(" TurnAnglesComponent-131-unitV: " + unit.UnitType + " / ( " + unit.Position.x + " , " + 0 + " , " + unit.Position.z + ")");

            }

            isSky = true;

        }

        public void Update()
        {
            //if (isSky)
            //{
            //    float ay = this.GetParent<Unit>().EulerAngles.y;
            //    if (ay > 180)
            //    {
            //        ay -= 360;
            //    }
            //    if (ay < -180)
            //    {
            //        ay += 360;
            //    }
            //    this.GetParent<Unit>().EulerAngles.y = ay;
            //}
        }

    }
}
