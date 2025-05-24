using Data.Abilities;

namespace Signals
{
    public class DisarmStatusEffectSignal: StatusEffectSignalBase
    {
        public DisarmStatusEffectSignal(EffectProcessorData effectProcessorData, AbilityEffectData abilityEffectData) : base(effectProcessorData, abilityEffectData)
        {
        }
    }
}