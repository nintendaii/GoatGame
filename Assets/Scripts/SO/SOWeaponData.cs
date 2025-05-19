using Data.Items;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "SOWeaponData", menuName = "SO/WeaponData", order = 0)]
    public class SOWeaponData : ScriptableObject
    {
        public WeaponItem WeaponItem;
    }
}