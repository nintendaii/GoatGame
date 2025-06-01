using Data.Abilities;

namespace Signals
{
    public class SummonUnitEffectSignal: StatusEffectSignalBase
    {
        public SummonUnitEffectSignal(EffectProcessorData effectProcessorData, AbilityEffectData abilityEffectData) : base(effectProcessorData, abilityEffectData)
        {
        }
    }
}