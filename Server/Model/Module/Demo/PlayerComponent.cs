using System.Collections.Generic;
using System.Linq;

namespace ETModel
{
	[ObjectSystem]
	public class PlayerComponentSystem : AwakeSystem<PlayerComponent>
	{
		public override void Awake(PlayerComponent self)
		{
			self.Awake();
		}
	}
	
	public class PlayerComponent : Component
	{
		public static PlayerComponent Instance { get; private set; }

		public Player MyPlayer;
		
		private readonly Dictionary<long, Player> idPlayers = new Dictionary<long, Player>();

		public void Awake()
		{
			Instance = this;
		}

        public void Add(Player player)
        {
            if (!idPlayers.Keys.Contains(player.Id))
            {
                this.idPlayers.Add(player.Id, player);
            }
        }

        public Player Get(long id)
        {
            this.idPlayers.TryGetValue(id, out Player gamer);
            return gamer;
        }
        public Player[] GetByUserId(long userid)
        {
            List<Player> players = new List<Player>();
            foreach(Player tem in this.idPlayers.Values.ToArray())
            {
                if(tem.UserId == userid)
                {
                    players.Add(tem);
                }
            }
            return players.ToArray();
        }

        public void Remove(long id)
		{
			this.idPlayers.Remove(id);
		}

		public int Count
		{
			get
			{
				return this.idPlayers.Count;
			}
		}

		public Player[] GetAll()
		{
			return this.idPlayers.Values.ToArray();
		}

		public override void Dispose()
		{
			if (this.IsDisposed)
			{
				return;
			}
			
			base.Dispose();

			foreach (Player player in this.idPlayers.Values)
			{
				player.Dispose();
			}

			Instance = null;
		}
	}
}