using System;
using System.Collections.Generic;

namespace ETModel
{
    public class InventoryDB : Component
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = "物品";
        public int RolerId { get; set; } = 100001;    //???
        public int InventoryId { get; set; } = 0;
        public Quality Quality { get; set; } = Quality.White;  //品质等级（白绿蓝紫橙）        
        public int Place { get; set; }  = 0;
        public int Exp { get; set; } = 0;
        public int Level { get; set; } = 0;
        public int Count { get; set; } = 1;                                   //物品数量
        public int Durability { get; set; } = 0;                              //装备耐久 = 2 * level
        public int Pice { get; set; } = 0;                                    //物品价格 = durability * 0.1; ????
        public string BuildDate { get; set; } = "";
       
    }
}
