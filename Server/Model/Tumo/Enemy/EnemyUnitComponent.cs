using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using ETHotfix;
using System.Net;

namespace ETModel
{
    [ObjectSystem]
    public class EnemyUnitComponentAwakeSystem : AwakeSystem<EnemyUnitComponent>
    {
        public override void Awake(EnemyUnitComponent self)
        {
            self.Awake();
        }
    }

    [ObjectSystem]
    public class EnemyUnitComponentStartSystem : StartSystem<EnemyUnitComponent>
    {
        public override void Start(EnemyUnitComponent self)
        {
            self.Start();
        }
    }

    public class EnemyUnitComponent : Component
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private readonly Dictionary<long, Unit> idUnits = new Dictionary<long, Unit>();

        public void Awake()   {    }

        public void Start()
        {
            Console.WriteLine(" EnemyUnitComponent-40: " + "map服务器 初始化小怪");

            /// map服务器 初始化小怪
            IPEndPoint mapAddress = StartConfigComponent.Instance.MapConfigs[0].GetComponent<InnerConfig>().IPEndPoint;
            Session mapSession = Game.Scene.GetComponent<NetInnerComponent>().Get(mapAddress);
            mapSession.Send(new M2M_CreateEnemyUnit() { Count = 4 });
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
        }

        public void Add(Unit unit)
        {
            this.idUnits.Add(unit.Id, unit);
        }

        public Unit Get(long id)
        {
            this.idUnits.TryGetValue(id, out Unit unit);
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
