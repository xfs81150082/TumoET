using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class CharacterControllerComponentUpdateSystem : UpdateSystem<CharacterControllerComponent>
    {
        public override void Update(CharacterControllerComponent self)
        {
            self.KeyMove();
        }
    }

    [ObjectSystem]
    public class CharacterControllerComponentAwakeSystem : AwakeSystem<CharacterControllerComponent>
    {
        public override void Awake(CharacterControllerComponent self)
        {
            self.Awake();
        }
    }

    public class CharacterControllerComponent : Component
    {
        public bool isCanControl = false;                                //表示是否可以用键盘控制
        public float h = 0;
        public float v = 0;
        public Vector3 moveDirection = Vector3.zero;
        public float roteSpeed = 5.0f;
        public float moveSpeed = 4.0f;
        public float jumpSpeed = 5.0f;
        public float gravity = 10.0f;
        public float gdy = 0.2f;
        public bool isZero = false;
        public CharacterController Controller;
        public AnimatorComponent animatorComponent;

        public float startTime = 0;
        public bool isStart = false;
        public float resTime = 1.0f;
        public Vector3 ClickPoint;
        public int mapMask;
        public readonly Move_Map move_Map = new Move_Map();

        public void Awake()
        {
            this.mapMask = LayerMask.GetMask("Map");
            Controller = this.GetParent<Unit>().GameObject.GetComponent<CharacterController>();

            Debug.Log(" CharacterControllerComponent-42: " + mapMask);
        }


    }
}
