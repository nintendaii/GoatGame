using Data.Abilities;

namespace Signals
{
    public class SilenceStatusEffectSignal: StatusEffectSignalBase
    {
        public SilenceStatusEffectSignal(EffectProcessorData effectProcessorData, AbilityEffectData abilityEffectData) : base(effectProcessorData, abilityEffectData)
        {
        }
    }
}