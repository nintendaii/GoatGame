using System;
using System.Collections.Generic;
using Data.Abilities;
using Turn;
using Unit;
using UnityEngine;
using Zenject;

namespace Abilities
{
    public class StatusEffectSystem
    {
        [Inject] private readonly TurnManager _turnManager;
        public Dictionary<UnitEntityController, List<StatusEffectApplicationData>> _statusEffectsContainer = new();

        public void ApplyStatusEffect(UnitEntityController target, StatusEffect effect, Action onEffectExpired = null, Action onEffectActivated = null)
        {
            var currentTurn = _turnManager.CurrentTurn;

            if (!_statusEffectsContainer.ContainsKey(target))
            {
                _statusEffectsContainer[target] = new List<StatusEffectApplicationData>();
            }

            Debug.Log($"Adding {effect.effectType} effect for {target.name}");
            _statusEffectsContainer[target].Add(new StatusEffectApplicationData
            {
                StatusEffectApplied = effect,
                ApplicationTurn = currentTurn,
                StatusEffectIteration = 0,
                OnEffectExpired = onEffectExpired,
                OnEffectActivated = onEffectActivated
            });
        }

        private void IncrementStatusEffect(UnitEntityController unit)
        {
            if (_statusEffectsContainer.ContainsKey(unit)) 
            {
                foreach (var e in _statusEffectsContainer[unit])
                {
                    e.StatusEffectIteration++;
                }
            }
        }

        public bool ValidateStatusEffects(UnitEntityController unit)
        {
            //validate status effects
            var isUnitAllowedToMove = true;
            IncrementStatusEffect(unit);
            AdvanceStatusEffects();
            if (!_statusEffectsContainer.ContainsKey(unit)) 
            {
                return true;
            }
            var statusEffects = _statusEffectsContainer[unit];

            //validate current unit movement
            foreach (var s in statusEffects)
            {
                if (s.StatusEffectApplied.effectType == AbilityEffect.Stun)
                {
                    isUnitAllowedToMove = false;
                }
            }

            return isUnitAllowedToMove;
        }

        private void AdvanceStatusEffects()
        {
            Action ac = default!;
            var currentTurn = _turnManager.CurrentTurn;
            var unitsToClean = new List<UnitEntityController>();

            foreach (var kvp in _statusEffectsContainer)
            {
                var unit = kvp.Key;
                var effects = kvp.Value;

                var expiredEffects = new List<StatusEffectApplicationData>();

                foreach (var s in effects)
                {
                    bool isExpired;

                    switch (s.StatusEffectApplied.iterationType)
                    {
                        case StatusEffectIterationType.EveryTurn:
                            isExpired = currentTurn - s.ApplicationTurn > s.StatusEffectApplied.duration;
                            break;
                        case StatusEffectIterationType.EverySelfTurn:
                            isExpired = s.StatusEffectIteration > s.StatusEffectApplied.duration;
                            break;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (isExpired)
                    {
                        unit.RemoveStatusEffect(s.StatusEffectApplied);
                        s.OnEffectExpired?.Invoke();
                        ac = s.OnEffectActivated;
                        Debug.Log($"{s.StatusEffectApplied.effectType} expired on {unit.name}");
                        expiredEffects.Add(s);
                    }
                }

                // Remove expired effects
                effects.RemoveAll(e => expiredEffects.Contains(e));

                // Mark unit for cleanup if no effects remain
                if (effects.Count == 0) unitsToClean.Add(unit);
            }

            // Cleanup empty units
            foreach (var unit in unitsToClean) _statusEffectsContainer.Remove(unit);
            ac?.Invoke();
        }

        public List<StatusEffectApplicationData> GetUnitStatusEffects(UnitEntityController unitEntityController)
        {
            return unitEntityController==null ? null : _statusEffectsContainer.GetValueOrDefault(unitEntityController);
        }

        public bool CheckIfUnitHasStatusEffect(UnitEntityController unitEntityController, AbilityEffect statusEffect)
        {
            var unit = GetUnitStatusEffects(unitEntityController);
            if (unit==null || unit.Count==0)
            {
                return false;
            }

            var effect = unit.Find(x => x.StatusEffectApplied.effectType == statusEffect);
            return effect != null;
        }
        
        public void DispelStatusEffect(UnitEntityController target, DispelStatusEffectTarget targetEffect)
        {
            var sToRemove = new List<StatusEffectApplicationData>();
            if (!_statusEffectsContainer.ContainsKey(target))
            {
                Debug.Log($"{target.name} has no Status Effects");
                return;
            }
            switch (targetEffect)
            {
                //TODO Handle dispel logic
                case DispelStatusEffectTarget.Positive:
                    foreach (var applicationData in _statusEffectsContainer[target])
                        if (applicationData.StatusEffectApplied.isDispelable &&
                            applicationData.StatusEffectApplied.isPositive)
                            sToRemove.Add(applicationData);
                    break;
                case DispelStatusEffectTarget.Negative:
                    foreach (var applicationData in _statusEffectsContainer[target])
                        if (applicationData.StatusEffectApplied.isDispelable &&
                            !applicationData.StatusEffectApplied.isPositive)
                            sToRemove.Add(applicationData);
                    break;
                case DispelStatusEffectTarget.All:
                    foreach (var applicationData in _statusEffectsContainer[target])
                        if (applicationData.StatusEffectApplied.isDispelable)
                            sToRemove.Add(applicationData);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetEffect), targetEffect, null);
            }

            foreach (var s in sToRemove)
            {
                s.OnEffectExpired?.Invoke();
            }
            _statusEffectsContainer[target].RemoveAll(effect => sToRemove.Contains(effect));
        }
    }
}