using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class ConditionAwakeSystem : AwakeSystem<Condition, System.Func<bool>>
    {
        public override void Awake(Condition self, System.Func<bool> a)
        {
            self.Awake(a);
        }
    }

    public class Condition : BtNode
    {
        public System.Func<bool> fn;

        public void Awake(System.Func<bool> fn)
        {
            this.fn = fn;
        }

        //public Condition() { }

        public Condition(System.Func<bool> fn)
        {
            this.fn = fn;
        }
        public override BtState Tick()
        {
            return fn() ? BtState.Success : BtState.Failure;
        }

        public override string ToString()
        {
            return "Condition : " + fn.Method.ToString();
        }
    }
}
