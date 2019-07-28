using System;
using System.Collections.Generic;
using System.Text;
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
        public Vector3 To;
        public Vector3 From;
        public long t = long.MaxValue;
        public long TurnTime = 1L;
        public Vector3 Target;

        public long StartTime;

        public long needTime;

        // 当前的移动速度
        public float ratateSpeed = 40.0f;


        public void Update()
        {
            UpdateTurn();
        }

        private void UpdateTurn()
        {
            //Log.Debug($"update turn: {this.t} {this.TurnTime}");
            if (this.t > this.TurnTime)
            {
                return;
            }

            //this.t += Time.deltaTime;

            Vector3 v = Vector3.Lerp(this.From, this.To, this.t / this.TurnTime);
            this.GetParent<Unit>().eulerAngles = v;
        }
        /// <summary>
        /// 改变Unit的朝向
        /// </summary>
        /// <param name="angle">与X轴正方向的夹角</param>
        public void Turn(Vector3 target)
        {
            this.To = target;
            this.From = this.GetParent<Unit>().eulerAngles;
            this.t = 0;
            //this.TurnTime = turnTime;
        }

    

    }
}
