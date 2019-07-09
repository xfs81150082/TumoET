using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public struct AoiUintInfo
    {
        public HashSet<long> MovesSet;

        public HashSet<long> OldMovesSet;

        public HashSet<long> Enters;

        public HashSet<long> Leaves;

        public void Dispose()
        {
            MovesSet?.Clear();

            OldMovesSet?.Clear();

            Enters?.Clear();

            Leaves?.Clear();
        }

    }
}