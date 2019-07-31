using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
    public class PositionMoveComponent : Component
    {
        public Vector3 To;
        public Vector3 From;
        public float t = float.MaxValue;
        public float moveTime;

        public void Update()
        {
            //MoveTo();
        }
        #region MoveTo
                 
        /// <summary>
        /// 改变Unit的位置
        /// </summary>
        public void MoveTo(Vector3 target)
        {
            this.GetParent<Unit>().Position = new Vector3(target.x, target.y, target.z);
        }

        //private void MoveTo()
        //{
        //    if (this.t > this.moveTime)
        //    {
        //        return;
        //    }

        //    this.t += Time.deltaTime;

        //    Vector3 vec = Vector3.Slerp(this.From, this.To, this.t / this.moveTime);
        //    this.GetParent<Unit>().Position = vec;
        //}

        /// <summary>
        /// 改变Unit的位置
        /// </summary>
        //public void MoveTo(Vector3 target, float moveTime = 0.1f)
        //{
        //    this.To = this.GetParent<Unit>().Position;
        //    this.From = target;
        //    this.t = 0;
        //    this.moveTime = moveTime;
        //}
        #endregion

    }
}
