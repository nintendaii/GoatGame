using Data.Abilities;

namespace Signals
{
    public class StatManipulationStatusEffectSignal: StatusEffectSignalBase
    {
        public StatManipulationStatusEffectSignal(EffectProcessorData effectProcessorData, AbilityEffectData abilityEffectData) : base(effectProcessorData, abilityEffectData)
        {
        }
    }
}