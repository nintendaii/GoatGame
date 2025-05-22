using Abilities;
using Data.Abilities;

namespace Signals
{
    public class DispelStatusEffectSignal: EffectUnitSignalBase
    {
        public AbilityEffectData AbilityEffectData;
        public DispelStatusEffectSignal(EffectProcessorData effectProcessorData, AbilityEffectData abilityEffectData) : base(effectProcessorData)
        {
            AbilityEffectData = abilityEffectData;
        }
    }
}