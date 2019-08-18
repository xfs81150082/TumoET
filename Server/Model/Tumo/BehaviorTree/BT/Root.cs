using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    //根节点，循环执行
    public class Root : Block
    {
        public bool isTerminated = false;

        public override BtState Tick()
        {
            if (isTerminated) { return BtState.Abort; }

            while (true)
            {
                switch (children[activeChild].Tick())
                {
                    case BtState.Continue:
                        return BtState.Continue;
                    case BtState.Abort:
                        isTerminated = true;
                        return BtState.Abort;
                    default:
                        activeChild++;
                        if (activeChild == children.Count)
                        {
                            activeChild = 0;
                            return BtState.Success;
                        }
                        continue;
                }
            }
        }


    }
}