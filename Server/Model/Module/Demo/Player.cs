using UnityEngine;

namespace ETModel
{
	[ObjectSystem]
	public class PlayerSystem : AwakeSystem<Player, string>
	{
		public override void Awake(Player self, string a)
		{
			self.Awake(a);
		}
	} 

    public sealed class Player : Entity
	{
		public string Account { get; set; }

        public long UserId { get; set; }

        public long UnitId { get; set; }

        public Vector3 spawnPosition;

        public Player() { }

        public Player(string account)
        {
            this.Account = account;
        }

        public void Awake(string account)
		{
			this.Account = account;
		}
		
        public void SetAccount(string account)
        {
            this.Account = account;
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