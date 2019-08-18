using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public abstract class Block : Branch
    {
        public override BtState Tick()
        {
            switch (children[activeChild].Tick())
            {
                case BtState.Continue:
                    return BtState.Continue;
                default:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return BtState.Success;
                    }
                    return BtState.Continue;
            }
        }
    }

}
