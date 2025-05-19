using System;
using Unity.VisualScripting;

namespace Data.Stats
{
    [Serializable]
    public class ElementalResistances
    {
        public StatBase Fire = new(StatType.FireResist, 0);
        public StatBase Ice = new(StatType.IceResist, 0);
        public StatBase Dark = new(StatType.DarkResist, 0);
        public StatBase Lightning = new(StatType.LightningResist, 0);

        public ElementalResistances Clone()
        {
            return new ElementalResistances()
            {
                Fire = Fire.Clone(),
                Ice = Ice.Clone(),
                Dark = Dark.Clone(),
                Lightning = Lightning.Clone()
            };
        }
    }
}