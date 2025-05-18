using Data;
using Data.Stats;
using UnityEngine;

namespace Formulas
{
    public static class DamageCalculator
    {
        private const float K = 100f; // Balance constant, can be adjusted by tier if necessary.

        /// <summary>
        /// Calculate Final Damage when Armor is Positive (Damage Reduction)
        /// </summary>
        /// <param name="baseDamage"></param>
        /// <param name="armor"></param>
        /// <returns></returns>
        private static float CalculateFinalDamageWithPositiveArmor(float baseDamage, float armor)
        {
            var damageReduction = armor / (armor + K);
            return baseDamage * (1 - damageReduction);
        }

        /// <summary>
        /// Calculate Final Damage when Armor is Negative (Damage Amplification)
        /// </summary>
        /// <param name="baseDamage"></param>
        /// <param name="armor"></param>
        /// <returns></returns>
        private static float CalculateFinalDamageWithNegativeArmor(float baseDamage, float armor)
        {
            var damageAmplification = Mathf.Abs(armor) / K;
            return baseDamage * (1 + damageAmplification);
        }

        /// <summary>
        /// Calculate Final Damage based on armor stat
        /// </summary>
        /// <param name="baseDamage"></param>
        /// <param name="armor"></param>
        /// <returns></returns>
        public static float CalculatePhysicalDamage(float baseDamage, float armor)
        {
            var value = armor >= 0
                ? CalculateFinalDamageWithPositiveArmor(baseDamage, armor)
                : CalculateFinalDamageWithNegativeArmor(baseDamage, armor);
            return value;
        }

        /// <summary>
        /// Calculate Final Elemental Damage when Resistance is Positive
        /// </summary>
        /// <param name="elementalDamage"></param>
        /// <param name="resistance"></param>
        /// <returns></returns>
        private static float CalculateElementalDamageWithPositiveResistance(float elementalDamage, float resistance)
        {
            return elementalDamage * (1 - resistance / 100f);
        }

        /// <summary>
        /// Calculate Final Elemental Damage when Resistance is Negative
        /// </summary>
        /// <param name="elementalDamage"></param>
        /// <param name="resistance"></param>
        /// <returns></returns>
        private static float CalculateElementalDamageWithNegativeResistance(float elementalDamage, float resistance)
        {
            return elementalDamage * (1 + Mathf.Abs(resistance) / 100f);
        }

        /// <summary>
        /// Calculate Final Elemental Damage based on resistance stat
        /// </summary>
        /// <param name="baseDamage"></param>
        /// <param name="resistance"></param>
        /// <returns></returns>
        public static float CalculateElementalDamage(float baseDamage, float resistance)
        {
            var value = resistance >= 0
                ? CalculateElementalDamageWithPositiveResistance(baseDamage, resistance)
                : CalculateElementalDamageWithNegativeResistance(baseDamage, resistance);
            return value;
        }

        /// <summary>
        /// Calculate Final Damage based on UnitData of performer and victim
        /// </summary>
        /// <param name="unitPerformer"></param>
        /// <param name="unitVictim"></param>
        /// <returns></returns>
        public static float CalculateAttackFinalDamage(UnitEntityStats unitPerformer, UnitEntityStats unitVictim)
        {
            var weapon = unitPerformer.Equipment.Weapon;
            var weaponDamage = weapon.GetTotalDamage(unitPerformer.GetAllStats());
            var physicalDamage = CalculatePhysicalDamage(weaponDamage, unitVictim.CoreStats.Armor.Value);
            var elementalFireDamage = CalculateElementalDamage(weapon.ElementalDamage.Fire.Value,
                unitVictim.CoreStats.Resistances.Fire.Value);
            var elementalIceDamage = CalculateElementalDamage(weapon.ElementalDamage.Ice.Value,
                unitVictim.CoreStats.Resistances.Ice.Value);
            var elementalDarkDamage = CalculateElementalDamage(weapon.ElementalDamage.Dark.Value,
                unitVictim.CoreStats.Resistances.Dark.Value);
            var elementalLightningDamage = CalculateElementalDamage(weapon.ElementalDamage.Lightning.Value,
                unitVictim.CoreStats.Resistances.Lightning.Value);
            return physicalDamage + elementalFireDamage + elementalLightningDamage + elementalIceDamage +
                   elementalDarkDamage;
        }

        /// <summary>
        /// Calculate Critical Strike Damage
        /// </summary>
        /// <param name="baseDamage"></param>
        /// <param name="criticalMultiplier"></param>
        /// <returns></returns>
        public static float CalculateCriticalStrikeDamage(float baseDamage, float criticalMultiplier)
        {
            return baseDamage * criticalMultiplier;
        }
    }
}