using Data.Abilities;
using Signals;

namespace Commands
{
    public class SpellImmunityStatusEffectCommand: ICommandWithParameters
    {
        public void Execute(ISignal signal)
        {
            var param = (SpellImmunityStatusEffectSignal)signal;
            var statusEffect = new StatusEffect
            {
                effectType = AbilityEffect.SpellImmunity,
                isDispelable = param.AbilityEffectData.isDispelable,
                isPositive = param.AbilityEffectData.isPositive,
                duration = param.AbilityEffectData.duration,
                iterationType = param.AbilityEffectData.IterationType
            };
            foreach (var t in param.EffectProcessorData.Targets)
            {
                t.ApplyStatusEffect(statusEffect);
            }
        }
    }
}