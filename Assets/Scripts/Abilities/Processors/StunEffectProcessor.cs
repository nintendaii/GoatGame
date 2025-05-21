using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities.Processors
{
    public class StunEffectProcessor: IAbilityEffectProcessor
    {
        public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            var targets = effectProcessorData.Targets;
            if (targets!=null)
            {
                var stunEffect = new StatusEffect
                {
                    effectType = AbilityEffect.Stun,
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