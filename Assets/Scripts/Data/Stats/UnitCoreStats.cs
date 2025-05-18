using System;

namespace Data
{
    [Serializable]
    public class UnitCoreStats
    {
        public StatBase Health = new StatBase(StatType.Health,0);
        public StatBase Armor = new StatBase(StatType.Armor,0);
        public ElementalResistances Resistances = new();
        public StatBase Speed = new StatBase(StatType.Speed,0);
        public StatBase PhysicalDamage = new StatBase(StatType.PhysicalDamage,0);
        public StatBase Evasion = new StatBase(StatType.Evasion,0);
        public StatBase CriticalStrikeChance = new StatBase(StatType.CriticalStrikeChance,0);
    }
}