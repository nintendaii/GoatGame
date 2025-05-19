using System;
using System.Linq;

namespace Data.Items
{
    [Serializable]
    public class EquipItem : ItemBase
    {
        public override string GetTooltip()
        {
            var bonuses = FlatBonuses.Count > 0
                ? "\nBonuses: " + string.Join(", ", FlatBonuses.Select(b => b.ToString()))
                : "";

            return $"{Name} Scaling: {bonuses}";
        }

        public EquipItem Clone()
        {
            return new EquipItem
            {
                Name = Name,
                Description = Description,
                Rarity = Rarity,
                ItemType = ItemType,
                FlatBonuses = FlatBonuses?.Select(b => b.Clone()).ToList(),
            };
        }
    }
}