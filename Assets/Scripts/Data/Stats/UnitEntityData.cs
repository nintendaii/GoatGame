using System;
using System.Collections.Generic;

namespace Data.Stats
{
    [Serializable]
    public class UnitEntityData
    {
        public string Id = Guid.NewGuid().ToString();
        public string Name;
        public UnitCoreStats CoreStats;
        public UnitMainStats MainStats;
        public EquipmentSet Equipment;
        public bool IsAlly;

        public Dictionary<StatType, float> GetAllStats()
        {
            var dic = new Dictionary<StatType, float>();
            dic.Add(CoreStats.PhysicalDamage.Type, CoreStats.PhysicalDamage.Value);
            dic.Add(CoreStats.Armor.Type, CoreStats.Armor.Value);
            dic.Add(CoreStats.Evasion.Type, CoreStats.Evasion.Value);
            dic.Add(CoreStats.Speed.Type, CoreStats.Speed.Value);
            dic.Add(CoreStats.ElementalResistances.Dark.Type, CoreStats.ElementalResistances.Dark.Value);
            dic.Add(CoreStats.ElementalResistances.Lightning.Type, CoreStats.ElementalResistances.Lightning.Value);
            dic.Add(CoreStats.ElementalResistances.Fire.Type, CoreStats.ElementalResistances.Fire.Value);
            dic.Add(CoreStats.ElementalResistances.Ice.Type, CoreStats.ElementalResistances.Ice.Value);

            dic.Add(CoreStats.ElementalDamage.Ice.Type, CoreStats.ElementalDamage.Ice.Value);
            dic.Add(CoreStats.ElementalDamage.Fire.Type, CoreStats.ElementalDamage.Fire.Value);
            dic.Add(CoreStats.ElementalDamage.Dark.Type, CoreStats.ElementalDamage.Dark.Value);
            dic.Add(CoreStats.ElementalDamage.Lightning.Type, CoreStats.ElementalDamage.Lightning.Value);

            dic.Add(CoreStats.CriticalStrikeChance.Type, CoreStats.CriticalStrikeChance.Value);

            dic.Add(MainStats.Intelligence.Type, MainStats.Intelligence.Value);
            dic.Add(MainStats.Agility.Type, MainStats.Agility.Value);
            dic.Add(MainStats.Strength.Type, MainStats.Strength.Value);
            dic.Add(MainStats.Level.Type, MainStats.Level.Value);
            return dic;
        }

        public StatBase GetStatByType(StatType type)
        {
            switch (type)
            {
                case StatType.Strength:
                    return MainStats.Strength;
                case StatType.Agility:
                    return MainStats.Agility;
                case StatType.Intelligence:
                    return MainStats.Intelligence;
                case StatType.Health:
                    return CoreStats.Health;
                case StatType.Armor:
                    return CoreStats.Armor;
                case StatType.Speed:
                    return CoreStats.Speed;
                case StatType.PhysicalDamage:
                    return CoreStats.PhysicalDamage;
                case StatType.FireResist:
                    return CoreStats.ElementalResistances.Fire;
                case StatType.IceResist:
                    return CoreStats.ElementalResistances.Ice;
                case StatType.DarkResist:
                    return CoreStats.ElementalResistances.Dark;
                case StatType.LightningResist:
                    return CoreStats.ElementalResistances.Lightning;
                case StatType.FireDamage:
                    return CoreStats.ElementalDamage.Fire;
                case StatType.IceDamage:
                    return CoreStats.ElementalDamage.Ice;
                case StatType.DarkDamage:
                    return CoreStats.ElementalDamage.Dark;
                case StatType.LightningDamage:
                    return CoreStats.ElementalDamage.Lightning;
                case StatType.Evasion:
                    return CoreStats.Evasion;
                case StatType.Level:
                    return MainStats.Level;
                case StatType.CriticalStrikeChance:
                    return CoreStats.CriticalStrikeChance;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}