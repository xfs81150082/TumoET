using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public abstract class Branch : BtNode
    {
        protected int activeChild;
        protected List<BtNode> children = new List<BtNode>();
        public virtual Branch OpenBranch(params BtNode[] children)
        {
            for (var i = 0; i < children.Length; i++)
            {
                this.children.Add(children[i]);
            }
            return this;
        }

        public List<BtNode> Children()
        {
            return children;
        }

        public int ActiveChild()
        {
            return activeChild;
        }

        public virtual void ResetChildren()
        {
            activeChild = 0;
            for (var i = 0; i < children.Count; i++)
            {
                Branch b = children[i] as Branch;
                if (b != null)
                {
                    b.ResetChildren();
                }
            }
        }
    }



}
