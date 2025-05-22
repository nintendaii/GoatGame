using Data.Abilities;

namespace Signals
{
    public abstract class StatusEffectSignalBase: EffectUnitSignalBase
    {
        public AbilityEffectData AbilityEffectData;
        public StatusEffectSignalBase(EffectProcessorData effectProcessorData, AbilityEffectData abilityEffectData) : base(effectProcessorData)
        {
            AbilityEffectData = abilityEffectData;
        }
    }
}