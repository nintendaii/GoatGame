using System.Linq;
using Abilities;
using Combat;
using Data.Abilities;
using Formulas;
using Global;
using Signals;
using Turn;
using UI;
using UnityEngine;
using Zenject;

namespace Commands
{
    public class DealAttackDamageUnitCommand: ICommandWithParameters
    {
        [Inject] private readonly CombatManager _combatManager;
        [Inject] private readonly UnitItemsUIContainer _unitItemsUIContainer;
        [Inject] private readonly TurnManager _turnManager;
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;
        [Inject] private readonly SignalBus _signalBus;

        public void Execute(ISignal signal)
        {
            var param = (DealAttackDamageUnitSignal)signal;
            
            if (param.Targets.Contains(param.Source))
            {
                Debug.LogWarning("Cant attack self");
                return;
            }

            if (_statusEffectSystem.CheckIfUnitHasStatusEffect(param.Source, AbilityEffect.Disarm))
            {
                Debug.LogWarning($"{param.Source} cant attack due to Disarm");
                return;
            }
            var filteredList = param.Targets.Where(x => x.IsAlive).ToList();
            if (filteredList.Count==0)
            {
                Debug.LogWarning("None of targets is alive in DealAttackDamageUnitSignal");
                return;
            }

            var isCritical =
                DamageCalculator.CalculateCriticalStrikeChance(param.Source.unitEntityData.CoreStats
                    .CriticalStrikeChance.Value);
            
            foreach (var u in filteredList)
            {
                var s = _statusEffectSystem.GetUnitStatusEffects(u);
                if (s?.Find(x=>x.StatusEffectApplied.effectType==AbilityEffect.PhysicalDamageImmunity) != null)
                {
                    Debug.Log($"{u.name} has PhysicalDamageImmunity so cant attack");
                    continue;
                }

                var isEvaded = DamageCalculator.CalculateEvasionChance(u.unitEntityData.CoreStats.Evasion.Value);
                if (isEvaded)
                {
                    Debug.Log($"{u.unitEntityData.Name} evaded attack!");
                    continue;
                }
                var damage = DamageCalculator.CalculateAttackFinalDamage(param.Source.unitEntityData,
                    u.unitEntityData);
                if (isCritical)
                {
                    Debug.Log("CRITICAL HIT!");
                    damage = DamageCalculator.CalculateCriticalStrikeDamage(damage, GlobalConstants.Combat.CRITICAL_HIT_MULTIPLIER);
                }
                
                u.DealDamage(damage);
                _unitItemsUIContainer.UpdateHealth(u.unitEntityData.CoreStats.Health.Value,u.unitEntityData.Id);
                if (!u.IsAlive)
                {
                    _turnManager.RemoveUnitFromQueue(u);
                }
            }
            
            _signalBus.Fire(new AdvanceNextTurnSignal());
        }
    }
}