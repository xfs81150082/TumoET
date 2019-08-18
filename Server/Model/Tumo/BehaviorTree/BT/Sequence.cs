using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{

    public class Sequence : Branch
    {
        public override BtState Tick()
        {
            var childState = children[activeChild].Tick();
            switch (childState)
            {
                case BtState.Success:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return BtState.Success;
                    }
                    else
                    {
                        return BtState.Continue;
                    }
                case BtState.Failure:
                    activeChild = 0;
                    return BtState.Failure;
                case BtState.Continue:
                    return BtState.Continue;
                case BtState.Abort:
                    activeChild = 0;
                    return BtState.Abort;
            }
            throw new System.Exception("This should never happen, but clearly it has.");
        }
    }

}
