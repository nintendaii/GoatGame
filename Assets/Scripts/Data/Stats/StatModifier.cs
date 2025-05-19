using System;
using System.Collections.Generic;

namespace Data.Stats
{
    [Serializable]
    public class StatModifier
    {
        public StatType Stat { get; }
        public StatScalingGrade ScalingGrade { get; }

        private static readonly Dictionary<StatScalingGrade, float> GradeMultipliers = new()
        {
            { StatScalingGrade.E, 0.25f },
            { StatScalingGrade.D, 0.5f },
            { StatScalingGrade.C, 0.75f },
            { StatScalingGrade.B, 1.0f },
            { StatScalingGrade.A, 1.25f },
            { StatScalingGrade.S, 1.5f }
        };

        public StatModifier(StatType stat, StatScalingGrade scalingGrade)
        {
            Stat = stat;
            ScalingGrade = scalingGrade;
        }

        public float CalculateBonus(float characterStat)
        {
            return characterStat * GradeMultipliers[ScalingGrade];
        }

        public override string ToString()
        {
            return $"{Stat} {ScalingGrade}";
        }
    }
}