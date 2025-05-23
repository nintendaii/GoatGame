using Data.Abilities;

namespace Signals
{
    public class SpellImmunityStatusEffectSignal: StatusEffectSignalBase
    {
        public SpellImmunityStatusEffectSignal(EffectProcessorData effectProcessorData, AbilityEffectData abilityEffectData) : base(effectProcessorData, abilityEffectData)
        {
        }
    }
}