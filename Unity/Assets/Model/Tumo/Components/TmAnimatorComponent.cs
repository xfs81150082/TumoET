using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
    public class TmAnimatorComponentStartSystem : StartSystem<TmAnimatorComponent>
    {
        public override void Start(TmAnimatorComponent self)
        {
            self.Start();
        }
    }

    public class TmAnimatorComponent : Component
    {
        public float h ;
        public float v ;

        public float moveSpeed = 3.0f;
        public bool isAttack = false;
        public bool isJump = false;
        public float margin = 0.2f;

        public Animator animator;
        public GameObject roler;

        public Queue<string> attackState = new Queue<string>();

        public void Start()
        {
            Unit unit = this.GetParent<Unit>();
            roler = unit.GameObject;
            animator = unit.GameObject.GetComponent<Animator>();
        }

    }
}
