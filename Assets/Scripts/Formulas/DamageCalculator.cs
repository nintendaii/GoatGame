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
        public static float CalculateAttackFinalDamage(UnitEntityData unitPerformer, UnitEntityData unitVictim)
        {
            var basePhysicalDamage = CalculatePhysicalDamage(unitPerformer.CoreStats.PhysicalDamage.Value, unitVictim.CoreStats.Armor.Value);
            var baseElementalFireDamage = CalculateElementalDamage(unitPerformer.CoreStats.ElementalDamage.Fire.Value, unitVictim.CoreStats.ElementalResistances.Fire.Value);
            var baseElementalIceDamage = CalculateElementalDamage(unitPerformer.CoreStats.ElementalDamage.Ice.Value, unitVictim.CoreStats.ElementalResistances.Ice.Value);
            var baseElementalDarkDamage = CalculateElementalDamage(unitPerformer.CoreStats.ElementalDamage.Dark.Value, unitVictim.CoreStats.ElementalResistances.Dark.Value);
            var baseElementalLightningDamage = CalculateElementalDamage(unitPerformer.CoreStats.ElementalDamage.Lightning.Value, unitVictim.CoreStats.ElementalResistances.Lightning.Value);
            var baseDamage = basePhysicalDamage + baseElementalFireDamage + baseElementalIceDamage +
                             baseElementalLightningDamage + baseElementalDarkDamage;
            var weapon = unitPerformer.Equipment.Weapon;
            var weaponDamage = 0f;
            if (weapon!=null || string.IsNullOrEmpty(weapon.Name))
            {
                weaponDamage = weapon.GetTotalDamage(unitPerformer.GetAllStats());
                var physicalDamage = CalculatePhysicalDamage(weaponDamage, unitVictim.CoreStats.Armor.Value);
                var elementalFireDamage = CalculateElementalDamage(weapon.ElementalDamage.Fire.Value,
                    unitVictim.CoreStats.ElementalResistances.Fire.Value);
                var elementalIceDamage = CalculateElementalDamage(weapon.ElementalDamage.Ice.Value,
                    unitVictim.CoreStats.ElementalResistances.Ice.Value);
                var elementalDarkDamage = CalculateElementalDamage(weapon.ElementalDamage.Dark.Value,
                    unitVictim.CoreStats.ElementalResistances.Dark.Value);
                var elementalLightningDamage = CalculateElementalDamage(weapon.ElementalDamage.Lightning.Value,
                    unitVictim.CoreStats.ElementalResistances.Lightning.Value);
                weaponDamage = physicalDamage + elementalFireDamage + elementalLightningDamage + elementalIceDamage +
                         elementalDarkDamage;
                Debug.Log($"Dealing WEAPON: P: {physicalDamage}, F: {elementalFireDamage} I: {elementalIceDamage} D: {elementalDarkDamage} L :{elementalLightningDamage}");

            }

            Debug.Log($"Dealing BASE: P: {basePhysicalDamage}, F: {baseElementalFireDamage} I: {baseElementalIceDamage} D: {baseElementalDarkDamage} L :{baseElementalLightningDamage}");
            Debug.Log($"Dealing TOTAL: {baseDamage+weaponDamage} to {unitVictim.Name}");
            return baseDamage + weaponDamage;
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
        
        /// <summary>
        /// Calculate Evasion Chance
        /// </summary>
        /// <param name="evasionChance">Chance to evade (0-1)</param>
        /// <returns>True if attack is evaded, false if hit</returns>
        public static bool CalculateEvasionChance(float evasionChance)
        {
            return Random.value < evasionChance;
        }
        
        /// <summary>
        /// Calculate Crit Chance
        /// </summary>
        /// <param name="criticalStrike">Chance for a crit (0-1)</param>
        /// <returns>True if crit, false not</returns>
        public static bool CalculateCriticalStrikeChance(float criticalStrike)
        {
            return Random.value < criticalStrike;
        }
    }
}