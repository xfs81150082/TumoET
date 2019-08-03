using System;
using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
	[ObjectSystem]
	public class AnimatorComponentAwakeSystem : AwakeSystem<AnimatorComponent>
	{
		public override void Awake(AnimatorComponent self)
		{
			self.Awake();
		}
	}

	[ObjectSystem]
	public class AnimatorComponentUpdateSystem : UpdateSystem<AnimatorComponent>
	{
		public override void Update(AnimatorComponent self)
		{
			self.Update();
		}
	}

	public class AnimatorComponent : Component
	{
		public Dictionary<string, AnimationClip> animationClips = new Dictionary<string, AnimationClip>();
		public HashSet<string> Parameter = new HashSet<string>();

		public MotionType MotionType;
		public float MontionSpeed;
		public bool isStop;
		public float stopSpeed;
		public Animator Animator;

        public void Awake()
        {
            Animator = this.GetParent<Unit>().GameObject.GetComponent<Animator>();
     
            #region
            //Animator animator = this.GetParent<Unit>().GameObject.GetComponent<Animator>();
            //if (animator == null)
            //{
            //    return;
            //}

            //if (animator.runtimeAnimatorController == null)
            //{
            //    return;
            //}

            //if (animator.runtimeAnimatorController.animationClips == null)
            //{
            //    return;
            //}
            //this.Animator = animator;
            //foreach (AnimationClip animationClip in animator.runtimeAnimatorController.animationClips)
            //{
            //    // 20190715 报空指针 修改增加 if 条件
            //    //if (animationClip == null)
            //    //{
            //    //    continue;
            //    //}
            //    //if (this.animationClips[animationClip.name] == null)
            //    //{
            //    //    continue;
            //    //}
            //    this.animationClips[animationClip.name] = animationClip;
            //}
            //foreach (AnimatorControllerParameter animatorControllerParameter in animator.parameters)
            //{
            //    this.Parameter.Add(animatorControllerParameter.name);
            //}
            #endregion
        }

        public void Update()
		{
            UpdatePlayerAnimator();
        }

        void UpdatePlayerAnimator()
        {
            if (this.isStop)
			{
				return;
			}

			if (this.MotionType == MotionType.None)
			{
				return;
			}

			try
			{
                ///2019.8.2
                this.AnimSet(this.MontionSpeed);
                this.AnimSet(this.MotionType.ToString());

                //this.Animator.SetFloat("Move", this.MontionSpeed);
                //this.Animator.SetTrigger(this.MotionType.ToString());

				this.MontionSpeed = 0;
				this.MotionType = MotionType.None;
			}
			catch (Exception ex)
			{
				throw new Exception($"动作播放失败: {this.MotionType}", ex);
			}
        }

        #region

        public void AnimSet(float v)
        {
            if (Math.Abs(v) < 0.05)
            {
                AnimSet("Idle");

            }
            else if (Math.Abs(v) < 0.5)
            {
                AnimSet("Walk", 0, v);
            }
            else
            {
                AnimSet("Run", 0, v);
            }
        }

        public void AnimSet( float h, float v)
        {
            AnimSet("Move", h, v);
        }

        public void AnimSet( string moveState)
        {
            AnimSet(moveState, 0, 0);
        }

        void AnimSet(string moveState, float h, float v)
        {
            switch (moveState)
            {
                case "Move":    //播放行走动画    
                    Animator.SetBool("Move", true);
                    Animator.SetFloat("Vblend", 1);
                    Animator.SetFloat("Hblend", 0);
                    Animator.SetBool("Attack", false);
                    break;
                case "Walk":    //播放行走动画    
                    Animator.SetBool("Move", true);
                    Animator.SetFloat("Vblend", 0.5f);
                    Animator.SetFloat("Hblend", 0);
                    Animator.SetBool("Attack", false);
                    break;
                case "Run":     //播放追击动画  
                    Animator.SetBool("Move", true);
                    Animator.SetFloat("Vblend", 1f);
                    Animator.SetFloat("Hblend", 0);
                    Animator.SetBool("Attack", false);
                    break;
                case "Attack":  //播放攻击动画
                    Animator.SetBool("Attack", true);
                    Animator.SetBool("Move", false);
                    break;
                case "Idle":    //播放休息动画
                    Animator.SetBool("Move", false);
                    Animator.SetBool("Attack", false);
                    break;
                case "Die":     //播放死亡动画 
                    Animator.SetTrigger("Die");
                    Animator.SetBool("Attack", false);
                    Animator.SetBool("Move", false);
                    break;
                case "Jump":    //播放跳跃动画 
                    Animator.SetTrigger("Jump");
                    break;
                case "Hit":    //播放挨打动画 
                    Animator.SetTrigger("Hit");
                    break;
                case "SkillOne":    //播放攻击特效1动画 
                    Animator.SetTrigger("SkillOne");
                    break;
                case "SkillTwo":    //播放攻击特效1动画 
                    Animator.SetTrigger("SkillTwo");
                    break;
                case "SkillThree":  //播放攻击特效1动画 
                    Animator.SetTrigger("SkillThree");
                    break;
                case "SkillZero":  //播放基本攻击动画 
                    Animator.SetTrigger("SkillZero");
                    break;
            }
        }





        #endregion



        #region ET
        public bool HasParameter(string parameter)
		{
			return this.Parameter.Contains(parameter);
		}

		public void PlayInTime(MotionType motionType, float time)
		{
			AnimationClip animationClip;
			if (!this.animationClips.TryGetValue(motionType.ToString(), out animationClip))
			{
				throw new Exception($"找不到该动作: {motionType}");
			}

			float motionSpeed = animationClip.length / time;
			if (motionSpeed < 0.01f || motionSpeed > 1000f)
			{
				Log.Error($"motionSpeed数值异常, {motionSpeed}, 此动作跳过");
				return;
			}
			this.MotionType = motionType;
			this.MontionSpeed = motionSpeed;
		}

		public void Play(MotionType motionType, float motionSpeed = 1f)
		{
            if (!this.HasParameter(motionType.ToString()))
			{
				return;
			}
			this.MotionType = motionType;
			this.MontionSpeed = motionSpeed;
		}

		public float AnimationTime(MotionType motionType)
		{
			AnimationClip animationClip;
			if (!this.animationClips.TryGetValue(motionType.ToString(), out animationClip))
			{
				throw new Exception($"找不到该动作: {motionType}");
			}
			return animationClip.length;
		}

		public void PauseAnimator()
		{
			if (this.isStop)
			{
				return;
			}
			this.isStop = true;

			if (this.Animator == null)
			{
				return;
			}
			this.stopSpeed = this.Animator.speed;
			this.Animator.speed = 0;
		}

		public void RunAnimator()
		{
			if (!this.isStop)
			{
				return;
			}

			this.isStop = false;

			if (this.Animator == null)
			{
				return;
			}
			this.Animator.speed = this.stopSpeed;
		}

		public void SetBoolValue(string name, bool state)
		{
            ///20190715
            if (Parameter == null) return;

            if (!this.HasParameter(name))
			{
				return;
			}

			this.Animator.SetBool(name, state);
		}

		public void SetFloatValue(string name, float state)
		{
            ///20190715
            if (Parameter == null) return;

            if (!this.HasParameter(name))
			{
				return;
			}

			this.Animator.SetFloat(name, state);
		}

		public void SetIntValue(string name, int value)
		{
            ///20190715
            if (Parameter == null) return;

            if (!this.HasParameter(name))
			{
				return;
			}

			this.Animator.SetInteger(name, value);
		}

		public void SetTrigger(string name)
		{
            ///20190715
            if (Parameter == null) return;

            if (!this.HasParameter(name))
			{
				return;
			}

			this.Animator.SetTrigger(name);
		}

		public void SetAnimatorSpeed(float speed)
		{
			this.stopSpeed = this.Animator.speed;
			this.Animator.speed = speed;
		}

		public void ResetAnimatorSpeed()
		{
			this.Animator.speed = this.stopSpeed;
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();

			this.animationClips = null;
			this.Parameter = null;
			this.Animator = null;
		}
        #endregion

    }
}