using System;
using System.Collections.Generic;

namespace Data.Items
{
    [Serializable]
    public abstract class ItemBase
    {
        public string Name;
        public string Description;
        public ItemRarity Rarity;
        public ItemType ItemType;
        public List<FlatStatBonus> FlatBonuses;


        public virtual List<FlatStatBonus> GetStatBonuses() => FlatBonuses;

        public abstract string GetTooltip();
    }
}