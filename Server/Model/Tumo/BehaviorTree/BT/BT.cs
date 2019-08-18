using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public static class BT
    {
        public static Root Root() { return new Root(); }
        public static Sequence Sequence() { return new Sequence(); }
        public static Selector Selector(bool shuffle = false) { return new Selector(shuffle); }

        public static Condition Condition(Func<bool> fn) { return new Condition(fn); }
        public static Operation Call(Action fn) { return new Operation(fn); }

        public static Trigger Trigger(string name, bool set = true) { return new Trigger(name, set); }     
    }

}
