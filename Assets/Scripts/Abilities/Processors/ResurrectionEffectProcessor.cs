using System;
using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities.Processors
{
    public class ResurrectionEffectProcessor: IAbilityEffectProcessor
    {
        public void Apply(UnitEntityController source, AbilityEffectData effectData, List<UnitEntityController> targets = null)
        {
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