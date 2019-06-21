using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ETModel
{
	[ObjectSystem]
	public class UnitComponentSystem : AwakeSystem<UnitComponent>
	{
		public override void Awake(UnitComponent self)
		{
			self.Awake();
		}
	}

    [ObjectSystem]
    public class UnitComponentChangeSystem : ChangeSystem<UnitComponent>
    {
        public override void Change(UnitComponent self)
        {
            self.Change();
        }
    }

    [ObjectSystem]
    public class UnitComponentUpdateSystem : UpdateSystem<UnitComponent>
    {
        public override void Update(UnitComponent self)
        {
            self.Update();
        }       
    }


    public class UnitComponent: Component
	{
		public static UnitComponent Instance { get; private set; }

		public Unit MyUnit;
		
		private readonly Dictionary<long, Unit> idUnits = new Dictionary<long, Unit>();

		public void Awake()
		{
			Instance = this;
		}

        public void Change()
        {
            if (MyUnit != null)
            {
                //ETModel.Game.EventSystem.Awake<Unit>(ETModel.Game.Scene.GetComponent<CameraComponent>(), MyUnit);      //将参数unit 传给组件CameraComponent awake方法
                Debug.Log(" UnitComponent-53-Change: " + MyUnit.Id);
            }
        }

        public void Update()
        {
            if (MyUnit != null)
            {
                //ETModel.Game.EventSystem.Awake<Unit>(ETModel.Game.Scene.GetComponent<CameraComponent>(), MyUnit);      //将参数unit 传给组件CameraComponent awake方法
                //Debug.Log(" UnitComponent-61: " + MyUnit.Id);
            }
            //Debug.Log(" UnitComponent-63: " + MyUnit.Id);

        }

        public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			base.Dispose();

			foreach (Unit unit in this.idUnits.Values)
			{
				unit.Dispose();
			}

			this.idUnits.Clear();

			Instance = null;
		}

		public void Add(Unit unit)
		{
			this.idUnits.Add(unit.Id, unit);
			unit.Parent = this;
		}

		public Unit Get(long id)
		{
			Unit unit;
			this.idUnits.TryGetValue(id, out unit);
			return unit;
		}

		public void Remove(long id)
		{
			Unit unit;
			this.idUnits.TryGetValue(id, out unit);
			this.idUnits.Remove(id);
			unit?.Dispose();
		}

		public void RemoveNoDispose(long id)
		{
			this.idUnits.Remove(id);
		}

		public int Count
		{
			get
			{
				return this.idUnits.Count;
			}
		}

		public Unit[] GetAll()
		{
			return this.idUnits.Values.ToArray();
		}
	}
}