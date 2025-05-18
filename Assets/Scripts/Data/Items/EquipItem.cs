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
    }
}