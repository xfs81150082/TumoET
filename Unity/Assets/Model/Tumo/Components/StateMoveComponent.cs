using ETModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class StateMoveComponentUpdateSystem : UpdateSystem<StateMoveComponent>
    {
        public override void Update(StateMoveComponent self)
        {
            self.Update();
        }
    }
    
    public class StateMoveComponent : Component
    {
        public List<Move_KeyCodeMap> mypath = new List<Move_KeyCodeMap>();

        public void Update()
        {
            Move();
        }                  

        public void Move()
        {
            while (mypath.Count > 0)
            {
                Move_KeyCodeMap map = this.mypath[0];
                Vector3 target = new Vector3(map.X, map.Y, map.Z);

                this.GetParent<Unit>().GetComponent<ClientMoveComponent>().MoveTo(target);
                this.GetParent<Unit>().GetComponent<TurnComponent>().Turn(map.AY);

                this.mypath.Remove(map);
            }
        }


    }
}
