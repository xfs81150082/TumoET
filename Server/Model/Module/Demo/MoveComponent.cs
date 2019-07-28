using System;
using System.Threading;
using UnityEngine;

namespace ETModel
{
    public class MoveComponent: Component
    {
        public Vector3 Target;

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
            if ((target - this.Target).sqrMagnitude < 0.01f)
            {
                return;
            }

            // 距离当前位置太近
            if ((this.GetParent<Unit>().Position - target).sqrMagnitude < 0.01f)
            {
                return;
            }
            
            this.Target = target;

            if (this.GetParent<Unit>().UnitType == UnitType.Player)
            {
                Console.WriteLine(" MoveComponent-51-tx/tz: " + " ( " + target.x + " , " + 0 + " , " + target.z + ")");
            }

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
            float distance = (this.Target - this.StartPos).magnitude;

            if (unit.UnitType == UnitType.Player)
            {
                Console.WriteLine(" MoveComponent-59-distance: " + distance);
            }

            if (Math.Abs(distance) < 0.1f)
            {
                return;
            }

            
            this.needTime = (long)(distance / this.moveSpeed * 1000);

            if (unit.UnitType == UnitType.Player)
            {
                Console.WriteLine(" MoveComponent-83-needTime: " + this.needTime);
            }

            TimerComponent timerComponent = Game.Scene.GetComponent<TimerComponent>();
            
            // 协程如果取消，将算出玩家的真实位置，赋值给玩家
            cancellationToken.Register(() =>
            {
                long timeNow = TimeHelper.Now();
                if (timeNow - this.StartTime >= this.needTime)
                {
                    unit.Position = this.Target;
                }
                else
                {
                    float amount = (timeNow - this.StartTime) * 1f / this.needTime;
                    unit.Position = Vector3.Lerp(this.StartPos, this.Target, amount);
                }
            });

            while (true)
            {
                await timerComponent.WaitAsync(50, cancellationToken); ///20190728 把50改为150 又改回为50
                
                long timeNow = TimeHelper.Now();
                
                if (timeNow - this.StartTime >= this.needTime)
                {
                    unit.Position = this.Target;
                    break;
                }

                float amount = (timeNow - this.StartTime) * 1f / this.needTime;
                unit.Position = Vector3.Lerp(this.StartPos, this.Target, amount);


                if (unit.UnitType == UnitType.Player)
                {
                    Console.WriteLine(" MoveComponent-121: " + unit.UnitType + " / ( " + unit.Position.x + " , " + 0 + " , " + unit.Position.z + ")");
                }
            }
        }
        

    }
}
