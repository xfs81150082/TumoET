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
    public class TranslateMoveComponentAwakeSystem : AwakeSystem<TranslateMoveComponent>
    {
        public override void Awake(TranslateMoveComponent self)
        {
            self.Awake();
        }
    }

    public class TranslateMoveComponent : Component
    {
        public float wsForword = 0.0f;
        public float adTrun = 0.0f;
        public float moveSpeed = 5.0f;
        public float roteSpeed = 5.0f;
        private readonly Frame_KeyCodeMap keyCode_TranslateMap = new Frame_KeyCodeMap();

        public Vector3 ClickPoint;
        public int mapMask;
        private readonly Frame_ClickMap frameClickMap = new Frame_ClickMap();

        public void Awake()
        {
            this.mapMask = LayerMask.GetMask("Map");
        }


    }
}
