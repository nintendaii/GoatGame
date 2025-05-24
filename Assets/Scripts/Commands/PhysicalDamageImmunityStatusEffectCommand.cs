using Abilities;
using Data.Abilities;
using Signals;
using Zenject;

namespace Commands
{
    public class PhysicalDamageImmunityStatusEffectCommand: ICommandWithParameters
    {
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;

        public void Execute(ISignal signal)
        {
            var param = (PhysicalDamageImmunityStatusEffectSignal)signal;
            var effectData = param.AbilityEffectData;
            var pDiEffect = new StatusEffect
            {
                effectType = AbilityEffect.PhysicalDamageImmunity,
                isDispelable = effectData.isDispelable,
                isPositive = effectData.isPositive,
                duration = effectData.duration,
                iterationType = effectData.IterationType
            };
            foreach (var t in param.EffectProcessorData.Targets)
            {
                _statusEffectSystem.ApplyStatusEffect(t, pDiEffect);
            }
        }
    }
}