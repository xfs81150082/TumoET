using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class ConditionAwakeSystem : AwakeSystem<Condition, Func<bool>>
    {
        public override void Awake(Condition self, Func<bool> a)
        {
            self.Awake(a);
        }
    }

    public class Condition : BtNode
    {
        public Func<bool> fn;

        public void Awake(Func<bool> fn)
        {
            this.fn = fn;
        }

        public Condition(Func<bool> fn)
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
