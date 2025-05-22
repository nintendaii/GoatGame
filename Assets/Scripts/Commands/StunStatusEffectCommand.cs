using Data.Abilities;
using Signals;

namespace Commands
{
    public class StunStatusEffectCommand: ICommandWithParameters
    {
        public void Execute(ISignal signal)
        {
            var param = (StunStatusEffectSignal)signal;
            var effectData = param.AbilityEffectData;
            var stunEffect = new StatusEffect
            {
                effectType = AbilityEffect.Stun,
                isDispelable = effectData.isDispelable,
                isPositive = effectData.isPositive,
                duration = effectData.duration,
                iterationType = effectData.IterationType
            };
            foreach (var t in param.EffectProcessorData.Targets)
            {
                t.ApplyStatusEffect(stunEffect);
            }
        }
    }
}