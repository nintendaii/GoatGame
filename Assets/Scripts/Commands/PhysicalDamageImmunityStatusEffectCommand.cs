using Data.Abilities;
using Signals;

namespace Commands
{
    public class PhysicalDamageImmunityStatusEffectCommand: ICommandWithParameters
    {
        public void Execute(ISignal signal)
        {
            var param = (PhysicalDamageImmunityStatusEffectSignal)signal;
            var effectData = param.AbilityEffectData;
            var pDIEffect = new StatusEffect
            {
                effectType = AbilityEffect.PhysicalDamageImmunity,
                isDispelable = effectData.isDispelable,
                isPositive = effectData.isPositive,
                duration = effectData.duration,
                iterationType = effectData.IterationType
            };
            foreach (var t in param.EffectProcessorData.Targets)
            {
                t.ApplyStatusEffect(pDIEffect);
            }
        }
    }
}