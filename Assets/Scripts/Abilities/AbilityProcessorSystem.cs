using System.Collections.Generic;
using Abilities.Processors;
using Data.Abilities;
using SO;
using Unit;
using UnityEngine;
using Zenject;

namespace Abilities
{
    public class AbilityProcessorSystem
    {
        private Dictionary<AbilityEffect, IAbilityEffectProcessor> processors;

        [Inject]
        public AbilityProcessorSystem(DiContainer container)
        {
            processors = new Dictionary<AbilityEffect, IAbilityEffectProcessor>
            {
                //{ AbilityEffect.Damage, new DamageEffectProcessor() },
                { AbilityEffect.Healing, container.Instantiate<HealingEffectProcessor>() },
                { AbilityEffect.Damage, container.Instantiate<DamageEffectProcessor>() },
                { AbilityEffect.Dispel, container.Instantiate<DispelEffectProcessor>() },
                { AbilityEffect.Stun, container.Instantiate<StunEffectProcessor>() },
                { AbilityEffect.PhysicalDamageImmunity, container.Instantiate<PhysicalDamageImmunityEffectProcessor>() },
                { AbilityEffect.SpellImmunity, container.Instantiate<SpellImmunityEffectProcessor>() },
                { AbilityEffect.Disarm, container.Instantiate<DisarmEffectProcessor>() },
                { AbilityEffect.Silence, container.Instantiate<SilenceEffectProcessor>() },
                { AbilityEffect.StatManipulation, container.Instantiate<StatManipulationEffectProcessor>() },
                { AbilityEffect.Resurrection, container.Instantiate<ResurrectionEffectProcessor>() },
                { AbilityEffect.Death, container.Instantiate<DeathEffectProcessor>() },
                { AbilityEffect.DelayedEffect, container.Instantiate<DelayedEffectEffectProcessor>() },
                { AbilityEffect.Summoning, container.Instantiate<SummoningEffectProcessor>() }
            };
        }

        public void UseAbility(UnitEntityController source, SOAbilityData ability, List<UnitEntityController> targets)
        {
            foreach (var effect in ability.effects)
            {
                if (processors.TryGetValue(effect.effectType, out var processor))
                    processor.Apply(source, effect, targets);
                else
                    Debug.LogWarning($"No processor for effect type {effect.effectType}");
            }
        }
    }
}