using System;
using Data.Stats;
using UnityEngine;

namespace Unit
{
    public class UnitEntityController : MonoBehaviour
    {
        public UnitEntityData unitEntityData;
        public Sprite unitAvatarSprite;

        public bool IsAlive => unitEntityData.CoreStats.Health.Value >= 0;

        private void Awake()
        {
            unitEntityData.Id = Guid.NewGuid().ToString();
            ApplyEquipmentStats();
        }

        private void ApplyEquipmentStats()
        {
            foreach (var e in unitEntityData.Equipment.Weapon.FlatBonuses)
            {
                var unitStat = unitEntityData.GetStatByType(e.StatType);
                unitStat.Value += e.Value;
            }
            foreach (var e in unitEntityData.Equipment.Chestplate.FlatBonuses)
            {
                var unitStat = unitEntityData.GetStatByType(e.StatType);
                unitStat.Value += e.Value;
            }
            foreach (var e in unitEntityData.Equipment.Gloves.FlatBonuses)
            {
                var unitStat = unitEntityData.GetStatByType(e.StatType);
                unitStat.Value += e.Value;
            }
            foreach (var e in unitEntityData.Equipment.Helmet.FlatBonuses)
            {
                var unitStat = unitEntityData.GetStatByType(e.StatType);
                unitStat.Value += e.Value;
            }
            foreach (var e in unitEntityData.Equipment.Ring.FlatBonuses)
            {
                var unitStat = unitEntityData.GetStatByType(e.StatType);
                unitStat.Value += e.Value;
            }
            foreach (var e in unitEntityData.Equipment.Boots.FlatBonuses)
            {
                var unitStat = unitEntityData.GetStatByType(e.StatType);
                unitStat.Value += e.Value;
            }
        }

        public void DealDamage(float damage)
        {
            unitEntityData.CoreStats.Health.Value -= damage;
        }
    }
}