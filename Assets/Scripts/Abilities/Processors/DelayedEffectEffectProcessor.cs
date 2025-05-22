using System;
using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities.Processors
{
    public class DelayedEffectEffectProcessor: IAbilityEffectProcessor
    {
        public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            if (effectProcessorData.Targets!=null)
            {
                var effect = new StatusEffect
                {
                    effectType = AbilityEffect.DelayedEffect,
                    delayedEffect = effectData.delayedEffectType,
                    isDispelable = effectData.isDispelable,
                    isPositive = effectData.isPositive,
                    duration = effectData.duration,
                    iterationType = effectData.IterationType
                };
                foreach (var t in effectProcessorData.Targets)
                {
                    t.ApplyStatusEffect(effect);
                }
            }
        }
    }
}