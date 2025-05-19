using System;
using Data.Items;

namespace Data.Stats
{
    [Serializable]
    public class EquipmentSet
    {
        public WeaponItem Weapon;
        public EquipItem Chestplate;
        public EquipItem Helmet;
        public EquipItem Gloves;
        public EquipItem Boots;
        public EquipItem Ring;

        public EquipmentSet Clone()
        {
            return new EquipmentSet
            {
                Weapon = Weapon,
                Chestplate = Chestplate,
                Helmet = Helmet,
                Gloves = Gloves,
                Boots = Boots,
                Ring = Ring
            };
        }
    }
}