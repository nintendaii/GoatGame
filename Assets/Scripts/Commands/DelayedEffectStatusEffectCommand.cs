using System.Collections.Generic;
using Abilities;
using Data.Abilities;
using Signals;
using Unit;
using UnityEngine;
using Zenject;

namespace Commands
{
    public class DelayedEffectStatusEffectCommand: ICommandWithParameters
    {
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;
        [Inject] private readonly AbilityProcessorSystem _abilityProcessorSystem;
        
        public void Execute(ISignal signal)
        {
            var param = (DelayedEffectStatusEffectSignal)signal;
            var effectData = param.AbilityEffectData;
            var effect = new StatusEffect
            {
                originAbility = param.EffectProcessorData.Ability,
                effectType = AbilityEffect.DelayedEffect,
                //delayedEffect = effectData.delayedEffectType,
                isDispelable = effectData.isDispelable,
                isPermanent = effectData.isPermanent,
                isPositive = effectData.isPositive,
                duration = effectData.duration,
                iterationType = effectData.IterationType
            };
            foreach (var t in param.EffectProcessorData.Targets)
            {
                _statusEffectSystem.ApplyStatusEffect(t, effect, onEffectActivated: () =>
                {
                    TriggerDelayedEffect(param.EffectProcessorData);
                });
            }
        }

        private void TriggerDelayedEffect(EffectProcessorData paramEffectProcessorData)
        {
            var ab = paramEffectProcessorData.Ability.Clone();
            var modifiedEffects = new List<AbilityEffectData>();
            foreach (var effect in ab.effects)
            {
                if (effect.hasDelayedEffect)
                {
                    var e = effect.Clone();
                    e.effectType = effect.delayedEffectType;
                    modifiedEffects.Add(e);
                }
            }

            ab.effects = modifiedEffects;
            paramEffectProcessorData.Ability = ab;
            Debug.Log("Triggering");
            _abilityProcessorSystem.UseAbility(paramEffectProcessorData);
        }
    }
}