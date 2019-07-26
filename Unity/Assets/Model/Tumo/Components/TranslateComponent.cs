using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class TranslateComponentUpdateSystem : UpdateSystem<TranslateComponent>
    {
        public override void Update(TranslateComponent self)
        {
            //self.KeyCodeContorlMove();

            self.SetMapMove();
        }
    }

    [ObjectSystem]
    public class TranslateComponentAwakeSystem : AwakeSystem<TranslateComponent>
    {
        public override void Awake(TranslateComponent self)
        {
            self.Awake();
        }
    }

    public class TranslateComponent : Component
    {
        public bool isCanControl = true;                   //表示是否可以用键盘控制
        public float h = 0;
        public float v = 0;
        public float moveSpeed = 4.0f;                      //移动速度
        public float roteSpeed = 5.0f;                      //旋转速度
        public TmAnimatorComponent animator;
        public GameObject roler;

        public float dy = 0.2f;                             //离地高度
        public bool IsJump = false;                          //触发跳跃条件？       
        public bool IsJumpUp = false;                        //在向上跳     
        public bool IsJumpDown = false;                      //在向下跳？       
        public float actuallySpeed = 0;                      //角色在跳跃过程中的实际速度       
        public float gravity = 10.0f;                         //角色速度递减的单位
        public float jumpSpeed = 40.0f;                       //角色跳跃速度//(起跳的一瞬间) 角色往上跳的速度

        public float startTime = 0;
        public bool isStart = false;
        public float resTime = 1.0f;
        public Vector3 ClickPoint;
        public int mapMask;
        public readonly Move_Map move_Map = new Move_Map();
        public bool isZero = false;                         //表示是否归零

        public void Awake()
        {
            this.mapMask = LayerMask.GetMask("Map");
            roler = this.GetParent<Unit>().GameObject;
            animator = this.GetParent<Unit>().GetComponent<TmAnimatorComponent>();
            animator.animator = this.GameObject.GetComponent<Animator>();

            Debug.Log(" TranslateComponent-55: " + mapMask);
        }


    }
}
