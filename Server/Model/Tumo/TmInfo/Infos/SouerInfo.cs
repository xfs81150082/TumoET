using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class SoulerInfo : Component
    {
        public Dictionary<long, Souler> Soulers = new Dictionary<long, Souler>();
        public Dictionary<long, SoulerItem> SoulerItems = new Dictionary<long, SoulerItem>();

        public SoulerInfo()
        {
            GetSkills();
            GetSkillItems();
        }

        void GetSkills()
        {

        }
        void GetSkillItems()
        {

        }


    }
}
