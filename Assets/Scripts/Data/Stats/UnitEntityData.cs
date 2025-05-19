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
            dic.Add(CoreStats.Speed.Type, CoreStats.Speed.Value);
            dic.Add(CoreStats.Resistances.Dark.Type, CoreStats.Resistances.Dark.Value);
            dic.Add(CoreStats.Resistances.Lightning.Type, CoreStats.Resistances.Lightning.Value);
            dic.Add(CoreStats.Resistances.Fire.Type, CoreStats.Resistances.Fire.Value);
            dic.Add(CoreStats.Resistances.Ice.Type, CoreStats.Resistances.Ice.Value);
            dic.Add(CoreStats.CriticalStrikeChance.Type, CoreStats.CriticalStrikeChance.Value);
            
            dic.Add(MainStats.Intelligence.Type, MainStats.Intelligence.Value);
            dic.Add(MainStats.Agility.Type, MainStats.Agility.Value);
            dic.Add(MainStats.Strength.Type, MainStats.Strength.Value);
            dic.Add(MainStats.Level.Type, MainStats.Level.Value);
            return dic;
        }
    }
}