using System;
using System.Collections.Generic;
using Data.Stats;

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


        public virtual List<FlatStatBonus> GetStatBonuses()
        {
            return FlatBonuses;
        }

        public abstract string GetTooltip();
    }
}