using PF;
using UnityEngine;

namespace ETModel
{
	public enum UnitType
	{
        Player,
        Monster,
		Npcer,
	}

	[ObjectSystem]
	public class UnitAwakeSystem : AwakeSystem<Unit, UnitType>
	{
		public override void Awake(Unit self, UnitType a)
		{
			self.Awake(a);
		}
	}

	public sealed class Unit: Entity
	{
		public UnitType UnitType { get; private set; }

        public Vector3 Position = Vector3.zero;

        public Vector3 eulerAngles = Vector3.zero;

        public void Awake(UnitType unitType)
		{
			this.UnitType = unitType;
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}

			base.Dispose();
		}
	}
}