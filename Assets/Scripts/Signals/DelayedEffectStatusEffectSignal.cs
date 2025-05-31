using Data.Abilities;

namespace Signals
{
    public class DelayedEffectStatusEffectSignal: StatusEffectSignalBase
    {
        public DelayedEffectStatusEffectSignal(EffectProcessorData effectProcessorData, AbilityEffectData abilityEffectData) : base(effectProcessorData, abilityEffectData)
        {
        }
    }
}