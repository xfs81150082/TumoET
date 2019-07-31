using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ETModel
{
    [ObjectSystem]
    public class TestComponentUpdateSystem : UpdateSystem<TestComponent>
    {
        public override void Update(TestComponent self)
        {
            //self.Update();
        }
    }

    public class TestComponent : Component
    {
        public void Update()
        {
            if (Game.Scene.GetComponent<UnitComponent>().Count > 0)
            {
                Unit unit = Game.Scene.GetComponent<UnitComponent>().GetAll()[0];

                Debug.Log(" TestComponent-x: " + unit.Rotation.x);
                Debug.Log(" TestComponent-y: " + unit.Rotation.y);
                Debug.Log(" TestComponent-z: " + unit.Rotation.z);
                Debug.Log(" TestComponent-w: " + unit.Rotation.w);
                Debug.Log(" TestComponent-eul-y: " + unit.Rotation.eulerAngles.y);



            }



        }
    }
}
