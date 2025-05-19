using Data.Stats;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "SOUnitData", menuName = "SO/UnitData", order = 0)]
    public class SOUnitData : ScriptableObject
    {
        public UnitEntityData unitEntityData;
        public Sprite unitAvatarSprite;
        public SOWeaponData WeaponData;

        public UnitEntityData Clone()
        {
            ApplyEquipment();
            var clone = unitEntityData.Clone();
            ApplyEquipmentStats(clone);
            return clone;
        }
        
        private void ApplyEquipment()
        {
            if (WeaponData!=null)
            {
                unitEntityData.Equipment.Weapon = WeaponData.WeaponItem.Clone();
            }
        }
        private void ApplyEquipmentStats(UnitEntityData data)
        {
            foreach (var e in data.Equipment.Weapon.FlatBonuses)
            {
                var unitStat = data.GetStatByType(e.StatType);
                unitStat.Value += e.Value;
            }
            
            foreach (var e in data.Equipment.Chestplate.FlatBonuses)
            {
                var unitStat = data.GetStatByType(e.StatType);
                unitStat.Value += e.Value;
            }
            foreach (var e in data.Equipment.Gloves.FlatBonuses)
            {
                var unitStat = data.GetStatByType(e.StatType);
                unitStat.Value += e.Value;
            }
            foreach (var e in data.Equipment.Helmet.FlatBonuses)
            {
                var unitStat = data.GetStatByType(e.StatType);
                unitStat.Value += e.Value;
            }
            foreach (var e in data.Equipment.Ring.FlatBonuses)
            {
                var unitStat = data.GetStatByType(e.StatType);
                unitStat.Value += e.Value;
            }
            foreach (var e in data.Equipment.Boots.FlatBonuses)
            {
                var unitStat = data.GetStatByType(e.StatType);
                unitStat.Value += e.Value;
            }
        }
    }
}