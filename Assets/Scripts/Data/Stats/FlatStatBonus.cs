using System;

namespace Data.Stats
{
    [Serializable]
    public class FlatStatBonus
    {
        public StatType Stat;
        public float Value;

        public FlatStatBonus(StatType stat, float value)
        {
            Stat = stat;
            Value = value;
        }

        public override string ToString()
        {
            return $"+{Value} {Stat}";
        }
    }
}