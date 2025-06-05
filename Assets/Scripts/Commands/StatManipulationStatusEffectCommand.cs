using System;
using Abilities;
using Data.Abilities;
using Data.Stats;
using Signals;
using UI;
using Unit;
using UnityEngine;
using Zenject;

namespace Commands
{
    public class StatManipulationStatusEffectCommand: ICommandWithParameters
    {
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;
        [Inject] private readonly UnitItemsUIContainer _itemsUIContainer;

        public void Execute(ISignal signal)
        {
            var param = (StatManipulationStatusEffectSignal)signal;
            var effectData = param.AbilityEffectData;
            var effect = new StatusEffect
            {
                originAbility = param.EffectProcessorData.Ability,
                effectType = AbilityEffect.StatManipulation,
                isDispelable = effectData.isDispelable,
                isPermanent = effectData.isPermanent,
                isPositive = effectData.isPositive,
                duration = effectData.duration,
                iterationType = effectData.IterationType
            };
            foreach (var t in param.EffectProcessorData.Targets)
            {
                ModifyStat(t, param.AbilityEffectData,param.AbilityEffectData.value);
                _statusEffectSystem.ApplyStatusEffect(t, effect, (() =>
                {
                    ModifyStat(t, param.AbilityEffectData, -param.AbilityEffectData.value);
                }));
            }
        }

        private void ModifyStat(UnitEntityController unit, AbilityEffectData abilityEffectData, float value)
        {
            unit.ManipulateStat(abilityEffectData.statAffected, value);
            var item = _itemsUIContainer.GetItemById(unit.unitEntityData.Id);
            item.UpdateData();
        }
    }
}