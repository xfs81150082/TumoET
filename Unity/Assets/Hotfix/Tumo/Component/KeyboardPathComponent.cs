using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETHotfix
{
    [ObjectSystem]
    public class KeyboardPathComponentAwakeSystem : AwakeSystem<KeyboardPathComponent>
    {
        public override void Awake(KeyboardPathComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class KeyboardPathComponentUpdateSystem : UpdateSystem<KeyboardPathComponent>
    {
        public override void Update(KeyboardPathComponent self)
        {
            self.SetMapMove();
        }
    }

    public class KeyboardPathComponent : Component
    {
        //public bool isCanControl = false;                     //表示是否可以用键盘控制
        public float h = 0;
        public float v = 0;

        //public float dy = 0.2f;                              //离地高度
        //public bool IsJump = false;                          //触发跳跃条件？       
        //public bool IsJumpUp = false;                        //在向上跳     
        //public bool IsJumpDown = false;                      //在向下跳？       
        //public float actuallySpeed = 0;                      //角色在跳跃过程中的实际速度       
        //public float gravity = 1.0f;                         //角色速度递减的单位
        //public float jumpSpeed = 14.0f;                       //角色跳跃速度//(起跳的一瞬间) 角色往上跳的速度

        public float startTime = 0;
        public bool isZero = false;
        public bool isStart = false;
        public float resTime = 1.0f;

        public Vector3 ClickPoint;
        public int mapMask;
        public readonly C2M_KeyboardPathResult c2M_PathKeyboardResult = new C2M_KeyboardPathResult();

        public void Awake()
        {
            this.mapMask = LayerMask.GetMask("Map");

            Debug.Log(" KeyboardPathComponent-45: " + mapMask);
        }

    }
}
