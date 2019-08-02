using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    public class MovePositionComponent : Component
    {
        public Vector3 TargetPosition;

        // 开启移动协程的时间
        public long StartTime;

        // 开启移动协程的Unit的位置
        public Vector3 StartPos;

        public long needTime;

        // 当前的移动速度
        public float moveSpeed = 4.0f;

        /// <summary>
        /// 异步 移动到 目标点
        /// </summary>
        /// <param name="target"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async ETTask MoveToAsync(Vector3 target, CancellationToken cancellationToken)
        {
            // 新目标点离旧目标点太近，不设置新的
            if ((target - this.TargetPosition).sqrMagnitude < 0.01f)
            {
                return;
            }

            // 距离当前位置太近
            if ((this.GetParent<Unit>().Position - target).sqrMagnitude < 0.01f)
            {
                return;
            }

            this.TargetPosition = target;

            //Console.WriteLine(" MovePositionComponent-46-tx/tz: " + " ( " + target.x + " , " + 0 + " , " + target.z + ")");

            // 开启协程移动
            await StartMove(cancellationToken);
        }

        // 开启协程移动,每100毫秒移动一次，并且协程取消的时候会计算玩家真实移动
        // 比方说玩家移动了250毫秒,玩家有新的目标,这时旧的移动协程结束,将计算250毫秒移动的位置，而不是300毫秒移动的位置
        public async ETTask StartMove(CancellationToken cancellationToken)
        {
            Unit unit = this.GetParent<Unit>();
            this.StartPos = unit.Position;
            this.StartTime = TimeHelper.Now();
            float distance = (this.TargetPosition - this.StartPos).magnitude;

            Vector3 target = new Vector3(this.TargetPosition.x, this.TargetPosition.y, this.TargetPosition.z);

            //Console.WriteLine(" MovePositionComponent-59-distance: " + distance);

            if (Math.Abs(distance) < 0.1f)
            {
                return;
            }


            this.needTime = (long)(distance / this.moveSpeed * 1000);

            //Console.WriteLine(" MovePositionComponent-71-needTime: " + this.needTime);

            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();

            // 协程如果取消，将算出玩家的真实位置，赋值给玩家
            cancellationToken.Register(() =>
            {
                long timeNow = TimeHelper.Now();
                if (timeNow - this.StartTime >= this.needTime)
                {
                    unit.Position = this.TargetPosition;
                }
                else
                {
                    float amount = (timeNow - this.StartTime) * 1f / this.needTime;
                    unit.Position = Vector3.Lerp(this.StartPos, this.TargetPosition, amount);
                }
            });

            while (true)
            {
                await timerComponent.WaitAsync(50, cancellationToken); ///20190728 把50改为150 又改回为50

                long timeNow = TimeHelper.Now();

                if (timeNow - this.StartTime >= this.needTime)
                {
                    unit.Position = this.TargetPosition;
                    break;
                }

                float amount = (timeNow - this.StartTime) * 1f / this.needTime;
                unit.Position = Vector3.Lerp(this.StartPos, this.TargetPosition, amount);

                Console.WriteLine(" MovePositionComponent-107-targetV: " + unit.UnitType + " / ( " + target.x + " , " + 0 + " , " + target.z + ")");
                Console.WriteLine(" MovePositionComponent-108-unitV: " + unit.UnitType + " / ( " + unit.Position.x + " , " + 0 + " , " + unit.Position.z + ")");
                Console.WriteLine(" MovePositionComponent-109-unitH: " + unit.UnitType + " / ( " + 0 + " , " + unit.EulerAngles.y + " , " + 0 + ")");

            }
        }


    }
}
