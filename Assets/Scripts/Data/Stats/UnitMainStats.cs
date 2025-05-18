using System;

namespace Data
{
    [Serializable]
    public class UnitMainStats
    {
        public StatBase Strength = new StatBase(StatType.Strength,0);
        public StatBase Agility = new StatBase(StatType.Agility,0);
        public StatBase Intelligence = new StatBase(StatType.Intelligence,0);
        public StatBase Level = new StatBase(StatType.Level,0);
    }
}