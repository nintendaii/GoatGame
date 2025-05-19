using System;
using Unity.VisualScripting;

namespace Data.Stats
{
    [Serializable]
    public class UnitCoreStats
    {
        public StatBase Health = new(StatType.Health, 0);
        public StatBase Armor = new(StatType.Armor, 0);
        public ElementalResistances ElementalResistances = new();
        public ElementalDamage ElementalDamage = new();
        public StatBase Speed = new(StatType.Speed, 0);
        public StatBase PhysicalDamage = new(StatType.PhysicalDamage, 0);
        public StatBase Evasion = new(StatType.Evasion, 0);
        public StatBase CriticalStrikeChance = new(StatType.CriticalStrikeChance, 0);

        public UnitCoreStats Clone()
        {
            return new UnitCoreStats
            {
                Health = Health.Clone(),
                Armor = Armor.Clone(),
                ElementalResistances = ElementalResistances.Clone(),
                ElementalDamage = ElementalDamage.Clone(),
                Speed = Speed.Clone(),
                PhysicalDamage = PhysicalDamage.Clone(),
                Evasion = Evasion.Clone(),
                CriticalStrikeChance = CriticalStrikeChance.Clone()
            };
        }
    }
}