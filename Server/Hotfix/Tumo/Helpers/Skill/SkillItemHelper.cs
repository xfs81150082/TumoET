using ETModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETHotfix
{
    public static class SkillItemHelper
    {
        public static void UpdateLevel(this SkillItem self, int level)
        {
            switch (self.GetComponent<Skill>().TribeType)
            {
                case TribeType.Valuation:
                    break;
                case TribeType.Case:
                    self.GetComponent<NumericComponent>().Set(NumericType.CaseBase, 14);
                    self.GetComponent<NumericComponent>().Set(NumericType.CaseAdd, level * 4);
                    break;
            }
        }
      

    }
}
