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
                    self.GetComponent<NumericComponent>().Set(NumericType.ValuationBase, 12);
                    self.GetComponent<NumericComponent>().Set(NumericType.ValuationAdd, level * 4);
                    break;
                case TribeType.Case:
                    self.GetComponent<NumericComponent>().Set(NumericType.CaseBase, 14);
                    self.GetComponent<NumericComponent>().Set(NumericType.CaseAdd, level * 4);
                    break;
            }
        }
      

    }
}
