using Abilities;
using Data.Abilities;
using Signals;
using Zenject;

namespace Commands
{
    public class DisarmStatusEffectCommand: ICommandWithParameters
    {
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;
        
        public void Execute(ISignal signal)
        {
            var param = (DisarmStatusEffectSignal)signal;
            var statusEffect = new StatusEffect
            {
                effectType = AbilityEffect.Disarm,
                isDispelable = param.AbilityEffectData.isDispelable,
                isPermanent = param.AbilityEffectData.isPermanent,
                isPositive = param.AbilityEffectData.isPositive,
                duration = param.AbilityEffectData.duration,
                iterationType = param.AbilityEffectData.IterationType
            };
            foreach (var t in param.EffectProcessorData.Targets)
            {
                _statusEffectSystem.ApplyStatusEffect(t, statusEffect);
            }
        }
    }
}