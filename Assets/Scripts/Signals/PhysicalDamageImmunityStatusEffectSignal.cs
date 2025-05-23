using Data.Abilities;

namespace Signals
{
    public class PhysicalDamageImmunityStatusEffectSignal: StatusEffectSignalBase
    {
        public PhysicalDamageImmunityStatusEffectSignal(EffectProcessorData effectProcessorData, AbilityEffectData abilityEffectData) : base(effectProcessorData, abilityEffectData)
        {
        }
    }
}