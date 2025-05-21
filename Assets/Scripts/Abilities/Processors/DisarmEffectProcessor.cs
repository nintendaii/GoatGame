using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities.Processors
{
    public class DisarmEffectProcessor: IAbilityEffectProcessor
    {
        public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            if (effectProcessorData.Targets!=null)
            {
                var stunEffect = new StatusEffect
                {
                    effectType = AbilityEffect.Disarm,
                    isDispelable = effectData.isDispelable,
                    isPositive = effectData.isPositive,
                    duration = effectData.duration
                };
                foreach (var t in effectProcessorData.Targets)
                {
                    t.ApplyStatusEffect(stunEffect);
                }
            }
        }
    }
}