using System.Collections.Generic;

namespace ETModel
{
    public struct AoiInfo
    {
        public HashSet<AoiNode> MovesSet;

        public HashSet<AoiNode> OldMovesSet;

        public HashSet<AoiNode> Enters;

        public HashSet<AoiNode> Leaves;

        public void Dispose()
        {
            MovesSet?.Clear();
            
            OldMovesSet?.Clear();
            
            Enters?.Clear();
            
            Leaves?.Clear();
        }
    }

    public struct AoiLink
    {
        public LinkedListNode<AoiNode> XNode;

        public LinkedListNode<AoiNode> YNode;

        public void Dispose()
        {
            XNode = null;

            YNode = null;
        }
    }
}