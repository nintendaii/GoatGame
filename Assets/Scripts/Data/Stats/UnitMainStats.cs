using System;
using Unity.VisualScripting;

namespace Data.Stats
{
    [Serializable]
    public class UnitMainStats
    {
        public StatBase Strength = new(StatType.Strength, 0);
        public StatBase Agility = new(StatType.Agility, 0);
        public StatBase Intelligence = new(StatType.Intelligence, 0);
        public StatBase Level = new(StatType.Level, 0);

        public UnitMainStats Clone()
        {
            return new UnitMainStats
            {
                Strength = Strength.Clone(),
                Agility = Agility.Clone(),
                Intelligence = Intelligence.Clone(),
                Level = Level.Clone()
            };
        }
    }
}