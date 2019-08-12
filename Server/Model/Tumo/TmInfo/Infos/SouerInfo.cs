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
            GetSoulers();
            GetSoulerItems();
        }

        void GetSoulers()
        {
            Souler inv11101 = ComponentFactory.CreateWithId<Souler>(11101);
            Souler inv21101 = ComponentFactory.CreateWithId<Souler>(21101);
            Souler inv31101 = ComponentFactory.CreateWithId<Souler>(31101);
            Souler inv41101 = ComponentFactory.CreateWithId<Souler>(41101);

            //inv11101.AddComponent<NumericComponent>().Set(NumericType.ManageAdd, 10);
            inv11101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            inv21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            inv21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            //inv31101.AddComponent<NumericComponent>().Set(NumericType.MeasureAdd, 10);
            inv31101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            inv41101.AddComponent<NumericComponent>().Set(NumericType.CaseAdd, 14);
            inv41101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 14);

            Soulers.Add(inv11101.Id, inv11101);
            Soulers.Add(inv21101.Id, inv21101);
            Soulers.Add(inv31101.Id, inv31101);
            Soulers.Add(inv41101.Id, inv41101);
        }

        void GetSoulerItems()
        {
            SoulerItem item11101 = ComponentFactory.CreateWithId<SoulerItem>(IdGenerater.GenerateId());
            SoulerItem item21101 = ComponentFactory.CreateWithId<SoulerItem>(IdGenerater.GenerateId());
            SoulerItem item31101 = ComponentFactory.CreateWithId<SoulerItem>(IdGenerater.GenerateId());
            SoulerItem item41101 = ComponentFactory.CreateWithId<SoulerItem>(IdGenerater.GenerateId());

            //item11101.AddComponent<NumericComponent>().Set(NumericType.ManageAdd, 10);
            item11101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            item21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            item21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            //item31101.AddComponent<NumericComponent>().Set(NumericType.MeasureAdd, 10);
            item31101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            item41101.AddComponent<NumericComponent>().Set(NumericType.CaseAdd, 14);
            item41101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 14);

            SoulerItems.Add(item11101.Id, item11101);
            SoulerItems.Add(item21101.Id, item21101);
            SoulerItems.Add(item31101.Id, item31101);
            SoulerItems.Add(item41101.Id, item41101);

        }

  
   


    }
}
