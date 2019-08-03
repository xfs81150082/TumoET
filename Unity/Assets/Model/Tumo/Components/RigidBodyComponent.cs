using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
    public class RigidBodyComponent
    {
        public Vector3 velocity;
        public Vector3 movePosition;
        public float addForce;
        public bool isStop;
        public Rigidbody rigidbody;
        public ETTaskCompletionSource moveTcs;

        public void Update()
        {
            if (this.isStop)
            {
                return;
            }

            try
            {
                if (this.velocity != null)
                {
                    rigidbody.velocity = velocity;
                }
                ///2019.8.2
                //this.AnimSet(this.MontionSpeed);
                //this.AnimSet(this.MotionType.ToString());


                //this.Animator.SetFloat("Move", this.MontionSpeed);

                //this.Animator.SetTrigger(this.MotionType.ToString());

                //this.MontionSpeed = 0;
                //this.MotionType = MotionType.None;
            }
            catch (Exception ex)
            {
                throw new Exception($"动作播放失败: {this.velocity}", ex);
            }
        }

    }
}
