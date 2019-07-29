using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class TurnComponentUpdateSystem : UpdateSystem<TurnComponent>
    {
        public override void Update(TurnComponent self)
        {
            self.Update();
        }
    }

    public class TurnComponent : Component
    {
        // turn
        // 当前的旋转速度
        public float ratateSpeed = 14.0f;

        public Vector3 Target;

        // 开启移动协程的时间
        public long StartTime;

        // 开启移动协程的Unit的位置
        public Vector3 StartPos;

        public long needTime;

        public bool isSky = true;

        /// <summary>
        /// 异步 移动到 目标点
        /// </summary>
        /// <param name="target"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ETTask MoveToAsync(Vector3 targetEulerAngles, CancellationToken cancellationToken)
        {
            isSky = false;

            // 新目标点离旧目标点太近，不设置新的
            if (Math.Abs(targetEulerAngles.y - this.Target.y) < 0.1f)
            {
                return;
            }

            // 距离当前位置太近
            if (Math.Abs(this.GetParent<Unit>().eulerAngles.y - targetEulerAngles.y) < 0.1f)
            {
                return;
            }

            this.Target = targetEulerAngles;

            Console.WriteLine(" TurnComponent-47-tx/tz: " + " ( " + 0 + " , " + targetEulerAngles.y + " , " + 0 + ")");

            // 开启协程移动
            await StartMove(cancellationToken);

        }

        // 开启协程移动,每100毫秒移动一次，并且协程取消的时候会计算玩家真实移动
        // 比方说玩家移动了250毫秒,玩家有新的目标,这时旧的移动协程结束,将计算250毫秒移动的位置，而不是300毫秒移动的位置
        public async ETTask StartMove(CancellationToken cancellationToken)
        {
            Unit unit = this.GetParent<Unit>();
            this.StartPos = unit.eulerAngles;
            this.StartTime = TimeHelper.Now();
            float angle = this.Target.y - this.StartPos.y;

            Console.WriteLine(" TurnComponent-65-distance: " + angle);

            if (Math.Abs(angle) < 0.1f)
            {
                return;
            }

            this.needTime = (long)(Math.Abs(angle) / this.ratateSpeed * 1000);

            Console.WriteLine(" TurnComponent-74-needTime: " + this.needTime);

            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();

            // 协程如果取消，将算出玩家的真实位置，赋值给玩家
            cancellationToken.Register(() =>
            {
                long timeNow = TimeHelper.Now();
                if (timeNow - this.StartTime >= this.needTime)
                {
                    unit.eulerAngles = this.Target;
                }
                else
                {
                    float amount = (timeNow - this.StartTime) * 1f / this.needTime;
                    unit.eulerAngles = Vector3.Lerp(this.StartPos, this.Target, amount);
                }

                isSky = true;

            });

            while (true)
            {
                await timerComponent.WaitAsync(50, cancellationToken); ///20190728 把50改为150 又改回为50

                long timeNow = TimeHelper.Now();

                if (timeNow - this.StartTime >= this.needTime)
                {
                    unit.eulerAngles = this.Target;
                    break;
                }

                float amount = (timeNow - this.StartTime) * 1f / this.needTime;
                unit.eulerAngles = Vector3.Lerp(this.StartPos, this.Target, amount);

                Console.WriteLine(" TurnComponent-108: " + unit.UnitType + " / ( " + 0 + " , " + unit.eulerAngles.y + " , " + 0 + ")");
            }

            isSky = true;

        }

        public void Update()
        {
            if (isSky)
            {
                float ay = this.GetParent<Unit>().eulerAngles.y;
                if (ay > 360)
                {
                    ay -= 360;
                }
                if (ay < 0)
                {
                    ay += 360;
                }
                this.GetParent<Unit>().eulerAngles.y = ay;
            }
        }

    }
}
