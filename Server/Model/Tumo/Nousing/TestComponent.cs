using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class TestComponentAwakeSystem : AwakeSystem<TestComponent>
    {
        public override void Awake(TestComponent self)
        {
            self.Awake();
        }

    }

    [ObjectSystem]
    public class TestComponentUpdateSystem : UpdateSystem<TestComponent>
    {
        public override void Update(TestComponent self)
        {
            self.Update();
        }
    }

    public class TestComponent : Component
    {
        public void Awake()
        {
            new TestOne();
        }

        public void Update()
        {

        }

    }
}
