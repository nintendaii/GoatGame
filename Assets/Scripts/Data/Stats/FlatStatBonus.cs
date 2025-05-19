using System;

namespace Data.Stats
{
    [Serializable]
    public class FlatStatBonus
    {
        public StatType StatType;
        public float Value;

        public FlatStatBonus(StatType statType, float value)
        {
            StatType = statType;
            Value = value;
        }

        public override string ToString()
        {
            return $"+{Value} {StatType}";
        }
    }
}