using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class ElementalResistances
    {
        public StatBase Fire = new StatBase(StatType.FireResist, 0);
        public StatBase Ice  = new StatBase(StatType.IceResist, 0);
        public StatBase Dark = new StatBase(StatType.DarkResist, 0);
        public StatBase Lightning = new StatBase(StatType.LightningResist, 0);

        public IEnumerable<StatBase> GetAll() => new[] { Fire, Ice, Dark, Lightning };
    }
}