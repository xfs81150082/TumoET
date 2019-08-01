using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class MovePositionComponentUpdateSystem : UpdateSystem<MovePositionComponent>
    {
        public override void Update(MovePositionComponent self)
        {
            self.Update();
        }
    }

    public class MovePositionComponent : Component
    {

        public Vector3 TargetPosition;
        // 开启移动协程的Unit的位置
        public Vector3 StartPos;
        // 开启移动协程的时间
        public long StartTime;
        public long needTime;
        public ETTaskCompletionSource moveTcs;

        public void Update()
        {
            MoveToAsync();
        }

        #region MoveToAsync
        void MoveToAsync()
        {
            if (this.moveTcs == null)
            {
                return;
            }

            Unit unit = this.GetParent<Unit>();
            long timeNow = TimeHelper.Now();

            if (timeNow - this.StartTime >= this.needTime)
            {
                unit.Position = this.TargetPosition;
                ETTaskCompletionSource tcs = this.moveTcs;
                this.moveTcs = null;
                tcs.SetResult();
                return;
            }

            float amount = (timeNow - this.StartTime) * 1f / this.needTime;
            unit.Position = Vector3.Lerp(this.StartPos, this.TargetPosition, amount);
        }

        public ETTask MoveToAsync(Vector3 target, float speedValue, CancellationToken cancellationToken)
        {
            Unit unit = this.GetParent<Unit>();

            if ((target - this.TargetPosition).magnitude < 0.1f)
            {
                return ETTask.CompletedTask;
            }

            this.TargetPosition = target;

            this.StartPos = unit.Position;
            this.StartTime = TimeHelper.Now();
            float distance = (this.TargetPosition - this.StartPos).magnitude;
            if (Math.Abs(distance) < 0.1f)
            {
                return ETTask.CompletedTask;
            }

            this.needTime = (long)(distance / speedValue * 1000);

            this.moveTcs = new ETTaskCompletionSource();

            cancellationToken.Register(() =>
            {
                this.moveTcs = null;
            });
            return this.moveTcs.Task;
        }
        #endregion





    }
}
