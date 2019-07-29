﻿using System;
using System.Threading;
using UnityEngine;

namespace ETModel
{
	[ObjectSystem]
	public class MoveComponentUpdateSystem : UpdateSystem<MoveComponent>
	{
		public override void Update(MoveComponent self)
		{
			self.Update();
		}
	}

	public class MoveComponent : Component
	{
		public Vector3 Target;
		// 开启移动协程的Unit的位置
		public Vector3 StartPos;
		// 开启移动协程的时间
		public long StartTime;
        public long needTime;
        public ETTaskCompletionSource moveTcs;


        public Vector3 To;
        public Vector3 From;
        public float t = float.MaxValue;
        public float moveTime;

		public void Update()
		{
            MoveToAsync();

            MoveTo();
        }
        #region MoveTo

        private void MoveTo()
        {
            if (this.t > this.moveTime)
            {
                return;
            }

            this.t += Time.deltaTime;

            Vector3 vec = Vector3.Slerp(this.From, this.To, this.t / this.moveTime);
            this.GetParent<Unit>().Position = vec;
        }

        /// <summary>
        /// 改变Unit的位置
        /// </summary>
        public void MoveTo(Vector3 target, float moveTime = 0.1f)
        {
            this.To = this.GetParent<Unit>().Position;
            this.From = target;
            this.t = 0;
            this.moveTime = moveTime;
        }  
        #endregion

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
                unit.Position = this.Target;
                ETTaskCompletionSource tcs = this.moveTcs;
                this.moveTcs = null;
                tcs.SetResult();
                return;
            }

            float amount = (timeNow - this.StartTime) * 1f / this.needTime;
            unit.Position = Vector3.Lerp(this.StartPos, this.Target, amount);
        }

        public ETTask MoveToAsync(Vector3 target, float speedValue, CancellationToken cancellationToken)
		{
			Unit unit = this.GetParent<Unit>();
			
			if ((target - this.Target).magnitude < 0.1f)
			{
				return ETTask.CompletedTask;
			}
			
			this.Target = target;

			
			this.StartPos = unit.Position;
			this.StartTime = TimeHelper.Now();
			float distance = (this.Target - this.StartPos).magnitude;
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