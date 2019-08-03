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
    public class TurnEulerAnglesComponentUpdateSystem : UpdateSystem<TurnEulerAnglesComponent>
    {
        public override void Update(TurnEulerAnglesComponent self)
        {
            self.Update();
        }
    }

    public class TurnEulerAnglesComponent : Component
    {
        public Vector3 TargetEulerAngles;
        // 开启移动协程的Unit的位置

        public Vector3 StartEul;
        // 开启移动协程的时间
        public long StartTime;
        public ETTaskCompletionSource turnTcs;

        public float time = 0 ;
        public float turnSpeed = 140.0f;
        public float needTime = 0.0f;


        public void Update()
        {
            TurnToAsync();
        }

        #region MoveToAsync
        void TurnToAsync()
        {
            if (this.turnTcs == null)
            {
                return;
            }

            Unit unit = this.GetParent<Unit>();
            this.time += Time.deltaTime ;

            if (this.time >= this.needTime)
            {
                unit.GameObject.transform.eulerAngles = this.TargetEulerAngles;

                ETTaskCompletionSource tcs = this.turnTcs;
                this.turnTcs = null;
                tcs.SetResult();
                return;
            }

            float amount = this.time * 1f / this.needTime;

            Quaternion from = PositionHelper.GetAngleToQuaternion(this.StartEul.y);
            Quaternion to = PositionHelper.GetAngleToQuaternion(this.TargetEulerAngles.y);

            Quaternion v = Quaternion.Slerp(from, to, amount);
            this.GetParent<Unit>().Rotation = v;

            //unit.GameObject.transform.eulerAngles = Vector3.Lerp(this.StartEul, this.TargetEulerAngles, amount);

            Debug.Log(" TurnEulerAnglesComponent-68-amount: " + this.time + " / " + this.needTime + " / " + amount);
            Debug.Log(" TurnEulerAnglesComponent-69-eulerAngles: " + "(" + 0 + ", " + unit.GameObject.transform.eulerAngles.y + ", " + 0 + " )");
        }

        public ETTask TurnToAsync(Vector3 target, float speedValue, CancellationToken cancellationToken)
        {
            Debug.Log(" TurnEulerAnglesComponent-73-target.y/speedValue: " + target.y + " / " + speedValue);


            Unit unit = this.GetParent<Unit>();
           
            // 新目标点离旧目标点太近，不设置新的
            if (Math.Abs(target.y - this.TargetEulerAngles.y) < 0.1f)
            {
                return ETTask.CompletedTask;
            }

            this.TargetEulerAngles = target;

            this.StartEul = unit.GameObject.transform.eulerAngles;

            this.time = 0;

            float angles = Math.Abs(this.TargetEulerAngles.y - this.StartEul.y);

             // 距离当前位置太近
            if (angles < 0.1f)
            {
                return ETTask.CompletedTask;
            }

            this.needTime = angles / turnSpeed  * speedValue/* * 1000*/;  ///其中speedValue接近1的数

            Debug.Log(" TurnEulerAnglesComponent-95-angles/turnTime: " + angles + " / " + this.needTime);

            this.turnTcs = new ETTaskCompletionSource();

            cancellationToken.Register(() =>
            {
                this.turnTcs = null;
            });
            return this.turnTcs.Task;
        }
        #endregion
         


    }
}
