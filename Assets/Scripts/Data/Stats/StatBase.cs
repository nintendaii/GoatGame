using System;

namespace Data.Stats
{
    [Serializable]
    public class StatBase
    {
        public StatType Type;
        public float Value;

        public StatBase(StatType type, float value)
        {
            Type = type;
            Value = value;
        }

        public override string ToString()
        {
            return $"{Type}: {Value}";
        }
    }
}