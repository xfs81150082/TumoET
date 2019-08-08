using System;
using System.Collections.Generic;
using System.Text;

namespace ETModel
{
    public class InventoryInfo : Component
    {
        public Dictionary<long, Inventory> Inventorys = new Dictionary<long, Inventory>();
        public Dictionary<long, InventoryItem> InventoryItems = new Dictionary<long, InventoryItem>();

        public InventoryInfo()
        {
            GetInventorys();
            GetInventoryItems();
        }

        void GetInventorys()
        {
            Inventory inv11101 = ComponentFactory.CreateWithId<Inventory>(11101);
            Inventory inv21101 = ComponentFactory.CreateWithId<Inventory>(21101);
            Inventory inv31101 = ComponentFactory.CreateWithId<Inventory>(31101);
            Inventory inv41101 = ComponentFactory.CreateWithId<Inventory>(41101);

            inv11101.AddComponent<NumericComponent>().Set(NumericType.ManageAdd, 10);
            inv11101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            inv21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            inv21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            inv31101.AddComponent<NumericComponent>().Set(NumericType.MeasureAdd, 10);
            inv31101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            inv41101.AddComponent<NumericComponent>().Set(NumericType.CaseAdd, 14);
            inv41101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 14);

            Inventorys.Add(inv11101.Id, inv11101);
            Inventorys.Add(inv21101.Id, inv21101);
            Inventorys.Add(inv31101.Id, inv31101);
            Inventorys.Add(inv41101.Id, inv41101);
        }

        void GetInventoryItems()
        {
            InventoryItem item11101 = ComponentFactory.CreateWithId<InventoryItem>(IdGenerater.GenerateId());
            InventoryItem item21101 = ComponentFactory.CreateWithId<InventoryItem>(IdGenerater.GenerateId());
            InventoryItem item31101 = ComponentFactory.CreateWithId<InventoryItem>(IdGenerater.GenerateId());
            InventoryItem item41101 = ComponentFactory.CreateWithId<InventoryItem>(IdGenerater.GenerateId());

            item11101.AddComponent<NumericComponent>().Set(NumericType.ManageAdd, 10);
            item11101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            item21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            item21101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 12);
            item31101.AddComponent<NumericComponent>().Set(NumericType.MeasureAdd, 10);
            item31101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 10);
            item41101.AddComponent<NumericComponent>().Set(NumericType.CaseAdd, 14);
            item41101.AddComponent<NumericComponent>().Set(NumericType.ValuationAdd, 14);

            InventoryItems.Add(item11101.Id, item11101);
            InventoryItems.Add(item21101.Id, item21101);
            InventoryItems.Add(item31101.Id, item31101);
            InventoryItems.Add(item41101.Id, item41101);
        }


    }
}
