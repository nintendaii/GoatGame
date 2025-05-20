using System;
using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities.Processors
{
    public class DelayedEffectEffectProcessor: IAbilityEffectProcessor
    {
        public void Apply(UnitEntityController source, AbilityEffectData effectData, List<UnitEntityController> targets = null)
        {
            if (targets!=null)
            {
                var effect = new StatusEffect
                {
                    effectType = AbilityEffect.DelayedEffect,
                    delayedEffect = effectData.delayedEffectType,
                    isDispelable = effectData.isDispelable,
                    isPositive = effectData.isPositive,
                    duration = effectData.duration
                };
                foreach (var t in targets)
                {
                    t.ApplyStatusEffect(effect);
                }
            }
        }
    }
}