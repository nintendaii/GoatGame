using Data.Abilities;

namespace Signals
{
    public class StunStatusEffectSignal: StatusEffectSignalBase
    {
        public StunStatusEffectSignal(EffectProcessorData effectProcessorData, AbilityEffectData abilityEffectData) : base(effectProcessorData, abilityEffectData)
        {
        }
    }
}