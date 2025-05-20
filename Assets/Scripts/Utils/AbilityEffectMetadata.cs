using System.Collections.Generic;
using Data.Abilities;

namespace Utils
{
    public static class AbilityEffectMetadata
    {
        private static readonly HashSet<AbilityEffect> _continuousEffects = new()
        {
            AbilityEffect.Stun,
            AbilityEffect.Silence,
            AbilityEffect.Disarm,
            AbilityEffect.PhysicalDamageImmunity,
            AbilityEffect.SpellImmunity,
            AbilityEffect.DelayedEffect,
        };
        
        private static readonly HashSet<AbilityEffect> _instantEffects = new()
        {
            AbilityEffect.Healing,
            AbilityEffect.Damage,
            AbilityEffect.Dispel,
            AbilityEffect.Summoning,
            AbilityEffect.SpellImmunity,
            AbilityEffect.Resurrection,
            AbilityEffect.Death
        };
        
        private static readonly HashSet<AbilityEffect> _combinedEffects = new()
        {
            AbilityEffect.StatManipulation
        };

        public static bool IsContinuous(AbilityEffect effect) => _continuousEffects.Contains(effect);
        public static bool IsInstant(AbilityEffect effect) => _instantEffects.Contains(effect);
        public static bool IsCombined(AbilityEffect effect) => _combinedEffects.Contains(effect);
    }

}