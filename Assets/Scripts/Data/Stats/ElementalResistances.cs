using System;
using System.Collections.Generic;

namespace Data.Stats
{
    [Serializable]
    public class ElementalResistances
    {
        public StatBase Fire = new(StatType.FireResist, 0);
        public StatBase Ice = new(StatType.IceResist, 0);
        public StatBase Dark = new(StatType.DarkResist, 0);
        public StatBase Lightning = new(StatType.LightningResist, 0);

        public IEnumerable<StatBase> GetAll()
        {
            return new[] { Fire, Ice, Dark, Lightning };
        }
    }
}