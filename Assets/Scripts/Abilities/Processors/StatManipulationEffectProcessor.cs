using System;
using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities.Processors
{
    public class StatManipulationEffectProcessor: IAbilityEffectProcessor
    {
        public void Apply(UnitEntityController source, AbilityEffectData effectData, List<UnitEntityController> targets = null)
        {
            if (targets!=null)
            {
                switch (effectData.effectDuration)
                {
                    case EffectDuration.Instant:
                        foreach (var t in targets)
                        {
                            t.ManipulateStat(effectData.statAffected, effectData.value);
                        }
                        break;
                    case EffectDuration.Continuous:
                        var effect = new StatusEffect
                        {
                            effectType = AbilityEffect.StatManipulation,
                            isDispelable = effectData.isDispelable,
                            isPositive = effectData.isPositive,
                            duration = effectData.duration
                        };
                        foreach (var t in targets)
                        {
                            t.ApplyStatusEffect(effect);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}