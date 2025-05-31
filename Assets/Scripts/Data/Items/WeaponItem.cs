using System;
using System.Collections.Generic;
using System.Linq;
using Data.Stats;

namespace Data.Items
{
    [Serializable]
    public class WeaponItem : ItemBase
    {
        public float BaseDamage;
        public ElementalDamage ElementalDamage = new();
        public List<StatModifier> ScalingModifiers;

        public float GetTotalDamage(Dictionary<StatType, StatBase> characterStats)
        {
            var bonus = 0f;
            foreach (var modifier in ScalingModifiers)
                if (characterStats.TryGetValue(modifier.Stat, out var statValue))
                    bonus += modifier.CalculateBonus(statValue.Value);
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

        public WeaponItem Clone()
        {
            return new WeaponItem
            {
                Name = Name,
                Description = Description,
                Rarity = Rarity,
                ItemType = ItemType,
                BaseDamage = BaseDamage,
                ElementalDamage = ElementalDamage.Clone(),
                FlatBonuses = FlatBonuses?.Select(b => b.Clone()).ToList(),
                ScalingModifiers = ScalingModifiers?.Select(m => m.Clone()).ToList()
            };
        }
    }
}