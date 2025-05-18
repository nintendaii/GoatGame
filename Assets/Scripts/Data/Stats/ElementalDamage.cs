using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class ElementalDamage
    {
        public StatBase Fire = new StatBase(StatType.FireDamage, 0);
        public StatBase Ice  = new StatBase(StatType.IceDamage, 0);
        public StatBase Dark = new StatBase(StatType.DarkDamage, 0);
        public StatBase Lightning = new StatBase(StatType.LightningDamage, 0);

        public IEnumerable<StatBase> GetAll() => new[] { Fire, Ice, Dark, Lightning };
    }
}