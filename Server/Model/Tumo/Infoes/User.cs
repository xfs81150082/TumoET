using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class User : Entity
    {
        public string Account { get; set; }
        public string Password { get; set; }
        public int Count { get; set; }
    }
}