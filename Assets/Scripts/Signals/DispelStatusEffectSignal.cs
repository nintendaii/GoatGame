using Abilities;
using Data.Abilities;

namespace Signals
{
    public class DispelStatusEffectSignal: StatusEffectSignalBase
    {
        public DispelStatusEffectSignal(EffectProcessorData effectProcessorData, AbilityEffectData abilityEffectData) : base(effectProcessorData, abilityEffectData)
        {
        }
    }
}