using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETModel;
using UnityEngine;

namespace ETModel
{
    public static class TmAnimatorComponentHelper
    {
        public static void AnimSet(this TmAnimatorComponent self, float v)
        {
            self.AnimSet("move", 0, v);
        }

        public static void AnimSet(this TmAnimatorComponent self, float h, float v)
        {
            self.AnimSet("move", h, v);
        }

        public static void AnimSet(this TmAnimatorComponent self, string moveState)
        {
            self.AnimSet(moveState, 0, 0);
        }

        static void AnimSet(this TmAnimatorComponent self, string moveState, float h, float v)
        {
            switch (moveState)
            {
                case "move":    //播放行走动画    
                    self.animator.SetBool("move", true);
                    self.animator.SetFloat("vblend", v);
                    self.animator.SetFloat("hblend", h);
                    self.animator.SetBool("attack", false);
                    //Debug.Log(" move：v" + v);
                    break;
                case "walk":    //播放行走动画    
                    self.animator.SetBool("move", true);
                    self.animator.SetFloat("vblend", -1f);
                    self.animator.SetFloat("hblend", 0);
                    self.animator.SetBool("attack", false);
                    break;
                case "run":     //播放追击动画  
                    self.animator.SetBool("move", true);
                    self.animator.SetFloat("vblend", 1f);
                    self.animator.SetFloat("hblend", 0);
                    self.animator.SetBool("attack", false);
                    break;
                case "attack":  //播放攻击动画
                    self.animator.SetBool("attack", true);
                    self.animator.SetBool("move", false);
                    break;
                case "idle":    //播放休息动画
                    self.animator.SetBool("move", false);
                    self.animator.SetBool("attack", false);
                    break;
                case "die":     //播放死亡动画 
                    self.animator.SetTrigger("die");
                    self.animator.SetBool("attack", false);
                    self.animator.SetBool("move", false);
                    break;
                case "jump":    //播放跳跃动画 
                    self.animator.SetTrigger("jump");
                    break;
                case "hit":    //播放挨打动画 
                    self.animator.SetTrigger("hit");
                    break;
                case "skillone":    //播放攻击特效1动画 
                    self.animator.SetTrigger("skillone");
                    break;
                case "skilltwo":    //播放攻击特效1动画 
                    self.animator.SetTrigger("skilltwo");
                    break;
                case "skillthree":  //播放攻击特效1动画 
                    self.animator.SetTrigger("skillthree");
                    break;
                case "skillbasic":  //播放基本攻击动画 
                    self.animator.SetTrigger("skillbasic");
                    break;
            }
        }


    }
}
