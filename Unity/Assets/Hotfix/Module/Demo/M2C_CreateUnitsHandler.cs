using ETModel;
using PF;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace ETHotfix
{
	[MessageHandler]
	public class M2C_CreateUnitsHandler : AMHandler<M2C_CreateUnits>
	{
		protected override void Run(ETModel.Session session, M2C_CreateUnits message)
		{	
			UnitComponent unitComponent = ETModel.Game.Scene.GetComponent<UnitComponent>();
			
			foreach (UnitInfo unitInfo in message.Units)
			{
				if (unitComponent.Get(unitInfo.UnitId) != null)
				{
					continue;
				}
				Unit unit = UnitFactory.Create(unitInfo.UnitId);
				unit.Position = new Vector3(unitInfo.X, unitInfo.Y, unitInfo.Z);

                
                ///20190621
                //为小骷髅添加数值组件  ///20190621
                NumericComponent numericComponent = unit.AddComponent<NumericComponent>();
                //为小骷髅设置生命值，这将触发数值改变事件
                numericComponent.Set(NumericType.HpBase, 10);


                ///20190619
                Debug.Log(" unit.InstanceId: " + unit.InstanceId + " unit.Id: " + unit.Id);
                Debug.Log(" M2C_CreateUnitsHandler-PlayerComponent.Instance.MyPlayer.UnitId: " + PlayerComponent.Instance.MyPlayer.UnitId);

                ETModel.Game.EventSystem.Awake<Unit>(ETModel.Game.Scene.GetComponent<CameraComponent>(), unit);      //将参数unit 传给组件CameraComponent awake方法

                if (unit.Id == PlayerComponent.Instance.MyPlayer.UnitId)
                {
                    UnitComponent.Instance.MyUnit = unit;
                }

            }
		}
	}
}
