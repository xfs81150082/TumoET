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
    public class EnemyUnitComponent : Component
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private readonly Dictionary<long, Unit> idUnits = new Dictionary<long, Unit>();

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

        void RemoveUnit(long id)
        {

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
        public Unit[] GetAllByIds(long[] unitIds)
        {
            HashSet<Unit> units = new HashSet<Unit>();
            foreach(long id in unitIds)
            {
                units.Add(Get(id));
            }
            return units.ToArray();
        }

    }
}
