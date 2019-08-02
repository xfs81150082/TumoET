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
        public Vector3 EulerAngles
        {
            get
            {
                return eulerAngles;
            }
            set
            {
                eulerAngles = value;
                if (eulerAngles.y > 180.0f)
                {
                    eulerAngles.y -= 360.0f;
                }
                if (eulerAngles.y < -180.0f)
                {
                    eulerAngles.y += 360.0f;
                }
            }
        }

        public Vector3 Position = Vector3.zero;

        private Vector3 eulerAngles = Vector3.zero;

        //public Quaternion ratation = Quaternion.identity;        

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