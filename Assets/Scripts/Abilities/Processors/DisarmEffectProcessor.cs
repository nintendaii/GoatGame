using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities.Processors
{
    public class DisarmEffectProcessor: IAbilityEffectProcessor
    {
        public void Apply(UnitEntityController source, AbilityEffectData effectData, List<UnitEntityController> targets = null)
        {
            if (targets!=null)
            {
                var stunEffect = new StatusEffect
                {
                    effectType = AbilityEffect.Disarm,
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