using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Items
{
    [Serializable]
    public class WeaponItem: ItemBase
    {
        public float BaseDamage;
        public ElementalDamage ElementalDamage = new();
        public List<StatModifier> ScalingModifiers;

        public float GetTotalDamage(Dictionary<StatType, float> characterStats)
        {
            float bonus = 0f;
            foreach (var modifier in ScalingModifiers)
            {
                if (characterStats.TryGetValue(modifier.Stat, out var statValue))
                {
                    bonus += modifier.CalculateBonus(statValue);
                }
            }
            return BaseDamage + bonus;
        }

        public WeaponItem()
        {
            ItemType = ItemType.Weapon;
        }

        public override string GetTooltip()
        {
            var scaling = string.Join(", ", ScalingModifiers.Select(m => m.ToString()));
            var bonuses = FlatBonuses.Count > 0
                ? "\nBonuses: " + string.Join(", ", FlatBonuses.Select(b => b.ToString()))
                : "";

            return $"{Name} Base Damage: {BaseDamage} Scaling: {scaling}{bonuses}";
        }

    }
}