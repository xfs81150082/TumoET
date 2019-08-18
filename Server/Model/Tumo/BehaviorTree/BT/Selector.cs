using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class Selector : Branch
    {
        public Selector(bool shuffle)
        {
            if (shuffle)
            {
                var n = children.Count;
                while (n > 1)
                {
                    n--;
                    Random rd = new Random();
                    var k = rd.Next(0, n + 1);
                    //var k = Mathf.FloorToInt(Random.value * (n + 1));
                    var value = children[k];
                    children[k] = children[n];
                    children[n] = value;
                }
            }
        }

        public override BtState Tick()
        {
            var childState = children[activeChild].Tick();
            switch (childState)
            {
                case BtState.Success:
                    activeChild = 0;
                    return BtState.Success;
                case BtState.Failure:
                    activeChild++;
                    if (activeChild == children.Count)
                    {
                        activeChild = 0;
                        return BtState.Failure;
                    }
                    else
                    {
                        return BtState.Continue;
                    }
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
