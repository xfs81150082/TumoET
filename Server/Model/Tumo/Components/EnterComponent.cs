using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class EnterComponent : Component
    {
        private readonly Queue<IActorMessage> enters = new Queue<IActorMessage>();

        public void Update()
        {
            while (enters.Count > 0)
            {

            }
        }

     


    }
}
