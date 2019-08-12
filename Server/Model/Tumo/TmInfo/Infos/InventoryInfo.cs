using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ETModel
{
    public class InventoryInfo : Component
    {
        public readonly Dictionary<long, Inventory> idInventorys = new Dictionary<long, Inventory>();

        public InventoryInfo()
        {
            GetInventorys();
        }

        void GetInventorys()
        {
            Inventory inv11101 = ComponentFactory.CreateWithId<Inventory>(11101);
            Inventory inv21101 = ComponentFactory.CreateWithId<Inventory>(21101);
            Inventory inv31101 = ComponentFactory.CreateWithId<Inventory>(31101);
            Inventory inv41101 = ComponentFactory.CreateWithId<Inventory>(41101);

            //inv11101.AddComponent<NumericComponent>().Set(NumericType.ManageAdd, 10);
            inv11101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            inv21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            inv21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            //inv31101.AddComponent<NumericComponent>().Set(NumericType.MeasureAdd, 10);
            inv31101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            inv41101.AddComponent<NumericComponent>().Set(NumericType.CaseAdd, 14);
            inv41101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 14);

            idInventorys.Add(inv11101.Id, inv11101);
            idInventorys.Add(inv21101.Id, inv21101);
            idInventorys.Add(inv31101.Id, inv31101);
            idInventorys.Add(inv41101.Id, inv41101);
        }

        public int Count
        {
            get { return idInventorys.Count; }
        }

        public Inventory[] GetAll()
        {
            return idInventorys.Values.ToArray();
        }

    }
}
