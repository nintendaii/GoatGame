using System;
using Data.Items;

namespace Data
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
    }
}