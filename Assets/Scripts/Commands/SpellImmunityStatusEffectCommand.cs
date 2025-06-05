using Abilities;
using Data.Abilities;
using Signals;
using Zenject;

namespace Commands
{
    public class SpellImmunityStatusEffectCommand: ICommandWithParameters
    {
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;

        public void Execute(ISignal signal)
        {
            var param = (SpellImmunityStatusEffectSignal)signal;
            var statusEffect = new StatusEffect
            {
                originAbility = param.EffectProcessorData.Ability,
                effectType = AbilityEffect.SpellImmunity,
                isDispelable = param.AbilityEffectData.isDispelable,
                isPositive = param.AbilityEffectData.isPositive,
                isPermanent = param.AbilityEffectData.isPermanent,
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