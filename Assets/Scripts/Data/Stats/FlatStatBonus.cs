using System;

namespace Data.Stats
{
    [Serializable]
    public class FlatStatBonus
    {
        public StatType StatType;
        public float Value;
        

        public override string ToString()
        {
            return $"+{Value} {StatType}";
        }

        public FlatStatBonus Clone()
        {
            return new FlatStatBonus
            {
                StatType = StatType,
                Value = Value
            };
        }
    }
}