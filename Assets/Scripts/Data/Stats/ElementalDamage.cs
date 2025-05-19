using System;

namespace Data.Stats
{
    [Serializable]
    public class ElementalDamage
    {
        public StatBase Fire = new(StatType.FireDamage, 0);
        public StatBase Ice = new(StatType.IceDamage, 0);
        public StatBase Dark = new(StatType.DarkDamage, 0);
        public StatBase Lightning = new(StatType.LightningDamage, 0);
    }
}