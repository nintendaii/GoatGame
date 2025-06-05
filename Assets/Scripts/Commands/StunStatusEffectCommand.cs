using Abilities;
using Data.Abilities;
using ModestTree;
using Signals;
using UnityEngine;
using Zenject;

namespace Commands
{
    public class StunStatusEffectCommand: ICommandWithParameters
    {
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;

        public void Execute(ISignal signal)
        {
            var param = (StunStatusEffectSignal)signal;
            var effectData = param.AbilityEffectData;
            var stunEffect = new StatusEffect
            {
                originAbility = param.EffectProcessorData.Ability,
                effectType = AbilityEffect.Stun,
                isDispelable = effectData.isDispelable,
                isPermanent = effectData.isPermanent,
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