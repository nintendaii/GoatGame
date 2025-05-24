using Abilities;
using Data.Abilities;
using Signals;
using Zenject;

namespace Commands
{
    public class SilenceStatusEffectCommand: ICommandWithParameters
    {
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;

        public void Execute(ISignal signal)
        {
            var param = (SilenceStatusEffectSignal)signal;
            var effectData = param.AbilityEffectData;
            var stunEffect = new StatusEffect
            {
                effectType = AbilityEffect.Silence,
                isDispelable = effectData.isDispelable,
                isPositive = effectData.isPositive,
                duration = effectData.duration,
                iterationType = effectData.IterationType
            };
            foreach (var t in param.EffectProcessorData.Targets)
            {
                _statusEffectSystem.ApplyStatusEffect(t, stunEffect);
            }
        }
    }
}