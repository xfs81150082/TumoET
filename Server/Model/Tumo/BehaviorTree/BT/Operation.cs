using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    [ObjectSystem]
    public class OperationAwakeSystem : AwakeSystem<Operation, Action>
    {
        public override void Awake(Operation self, Action a)
        {
            self.Awake(a);
        }
    }

    public class Operation : BtNode
    {
        Action fn;
        Func<IEnumerator<BtState>> coroutineFactory;
        IEnumerator<BtState> coroutine;

        public void Awake(Action fn)
        {
            this.fn = fn;
        }

        public Operation(System.Action fn)
        {
            this.fn = fn;
        }

        public Operation(System.Func<IEnumerator<BtState>> coroutineFactory)
        {
            this.coroutineFactory = coroutineFactory;
        }

        public override BtState Tick()
        {
            if (fn != null)
            {
                fn();
                return BtState.Success;
            }
            else
            {
                if (coroutine == null)
                    coroutine = coroutineFactory();
                if (!coroutine.MoveNext())
                {
                    coroutine = null;
                    return BtState.Success;
                }
                var result = coroutine.Current;
                if (result == BtState.Continue)
                    return BtState.Continue;
                else
                {
                    coroutine = null;
                    return result;
                }
            }
        }

        public override string ToString()
        {
            return "Action : " + fn.Method.ToString();
        }
    }

}
