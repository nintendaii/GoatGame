using System;
using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities.Processors
{
    public class ResurrectionEffectProcessor: IAbilityEffectProcessor
    {
        public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            var targets = effectProcessorData.Targets;
            if (targets!=null)
            {
                foreach (var t in targets)
                {
                    t.Resurrect();
                }
            }
        }
    }
}