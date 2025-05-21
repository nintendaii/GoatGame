using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities.Processors
{
    public class SilenceEffectProcessor: IAbilityEffectProcessor
    {
        public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            var targets = effectProcessorData.Targets;
            if (targets!=null)
            {
                var stunEffect = new StatusEffect
                {
                    effectType = AbilityEffect.Silence,
                    isDispelable = effectData.isDispelable,
                    isPositive = effectData.isPositive,
                    duration = effectData.duration
                };
                foreach (var t in targets)
                {
                    t.ApplyStatusEffect(stunEffect);
                }
            }
        }
    }
}