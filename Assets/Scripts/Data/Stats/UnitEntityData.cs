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

        public UnitEntityData Clone()
        {
            return new UnitEntityData
            {
                Id = Guid.NewGuid().ToString(),
                Name = Name,
                CoreStats = CoreStats.Clone(),
                MainStats = MainStats.Clone(),
                Equipment = Equipment.Clone()
            };
        }

        public void ApplyEquipment(EquipmentSet equipmentSet)
        {
            Equipment = equipmentSet;
        }
        public Dictionary<StatType, StatBase> GetAllStats()
        {
            var dic = new Dictionary<StatType, StatBase>
            {
                { CoreStats.PhysicalDamage.Type, CoreStats.PhysicalDamage },
                { CoreStats.Health.Type, CoreStats.Health },
                { CoreStats.Armor.Type, CoreStats.Armor },
                { CoreStats.Evasion.Type, CoreStats.Evasion },
                { CoreStats.Speed.Type, CoreStats.Speed },
                { CoreStats.ElementalResistances.Dark.Type, CoreStats.ElementalResistances.Dark },
                { CoreStats.ElementalResistances.Lightning.Type, CoreStats.ElementalResistances.Lightning },
                { CoreStats.ElementalResistances.Fire.Type, CoreStats.ElementalResistances.Fire },
                { CoreStats.ElementalResistances.Ice.Type, CoreStats.ElementalResistances.Ice },
                { CoreStats.ElementalDamage.Ice.Type, CoreStats.ElementalDamage.Ice },
                { CoreStats.ElementalDamage.Fire.Type, CoreStats.ElementalDamage.Fire },
                { CoreStats.ElementalDamage.Dark.Type, CoreStats.ElementalDamage.Dark },
                { CoreStats.ElementalDamage.Lightning.Type, CoreStats.ElementalDamage.Lightning },
                { CoreStats.CriticalStrikeChance.Type, CoreStats.CriticalStrikeChance },
                { MainStats.Intelligence.Type, MainStats.Intelligence },
                { MainStats.Agility.Type, MainStats.Agility },
                { MainStats.Strength.Type, MainStats.Strength },
                { MainStats.Level.Type, MainStats.Level }
            };

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