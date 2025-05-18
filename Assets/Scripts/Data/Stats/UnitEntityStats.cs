using System;
using System.Collections.Generic;
using System.Linq;

namespace Data
{
    [Serializable]
    public class UnitEntityStats
    {
        public UnitCoreStats CoreStats;
        public UnitMainStats MainStats;
        public EquipmentSet Equipment;

        public Dictionary<StatType, float>  GetAllStats()
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
            return dic;
        }
    }
}