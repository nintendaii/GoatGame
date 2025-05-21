using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities.Processors
{
    public class DeathEffectProcessor: IAbilityEffectProcessor
    {
        public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            if (effectProcessorData.Targets!=null)
            {
                foreach (var t in effectProcessorData.Targets)
                {
                    t.Kill();
                }
            }
        }
    }
}