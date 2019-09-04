using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class Userdb : ComponentWithId
    {
        public string account = "tumo";
        public string password = "123456";
        public long phonenumber = 13655123801;
        public string createdate = "";
        public List<long> playerids = new List<long>();
    }
}
