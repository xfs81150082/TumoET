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
            GetSkills();
            GetSkillItems();
        }

        void GetSkills()
        {

        }
        void GetSkillItems()
        {

        }

        /// <summary>
        /// 先向 BD 服务器 初始化小怪数据
        /// </summary>
        /// <param name="self"></param>
        void GetEnemyFromBD()
        {
            try
            {
                ///生产 Enemy 数据 Info
                GetEnemys(4);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        void GetEnemys(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Inventory enemy = ComponentFactory.Create<Inventory>();

                ///20190702
                enemy.AddComponent<NumericComponent>();


                SetNumeric(enemy, new object());

                Inventorys.Add(enemy.Id, enemy);
            }

        }
        void SetNumeric(Inventory enemy, object obj)
        {
            if (enemy.GetComponent<NumericComponent>() == null) return;
            NumericComponent num = enemy.GetComponent<NumericComponent>();
            /// 二次赋值
            num.Set(NumericType.ValuationAdd, 80);       // HpAdd 数值,进行赋值
        }




    }
}
