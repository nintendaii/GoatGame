using System;

namespace Data
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

        public override string ToString() => $"{Type}: {Value}";
    }
}