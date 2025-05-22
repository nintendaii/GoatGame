using System;
using System.Collections.Generic;
using Data.Abilities;
using Data.Turn;
using Turn;
using Unit;
using Zenject;

namespace Abilities
{
    public class StatusEffectSystem
    {
        [Inject] private readonly TurnManager _turnManager;
        private Dictionary<UnitEntityController, List<StatusEffectApplicationData>> _statusEffectsContainer = new();

        public void ApplyStatusEffect(UnitEntityController target, StatusEffect effect)
        {
            var currentTurn = _turnManager.CurrentTurn;
            _statusEffectsContainer[target].Add(new StatusEffectApplicationData{StatusEffectApplied = effect, ApplicationTurn = currentTurn, StatusEffectIteration = 0});
        }

        public bool ValidateStatusEffects(UnitEntityController unit)
        {
            //validate status effects
            var statusEffects = _statusEffectsContainer[unit];
            AdvanceStatusEffects();
            
            //validate current unit movement
            var isUnitAllowedToMove = true;
            foreach (var s in statusEffects)
            {
                if (s.StatusEffectApplied.effectType==AbilityEffect.Stun)
                {
                    isUnitAllowedToMove = false;
                }
            }

            return isUnitAllowedToMove;
        }

        private void AdvanceStatusEffects()
        {
            var currentTurn = _turnManager.CurrentTurn;
            foreach (var kvp in _statusEffectsContainer)
            {
                foreach (var s in kvp.Value)
                {
                    switch (s.StatusEffectApplied.iterationType)
                    {
                        case StatusEffectIterationType.EveryTurn:
                            if (currentTurn-s.ApplicationTurn>=s.StatusEffectApplied.duration)
                            {
                                kvp.Key.RemoveStatusEffect(s.StatusEffectApplied);
                            }
                            break;
                        case StatusEffectIterationType.EverySelfTurn:
                            s.StatusEffectIteration++;
                            if (s.StatusEffectIteration>=s.StatusEffectApplied.duration)
                            {
                                kvp.Key.RemoveStatusEffect(s.StatusEffectApplied);
                            }
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }
        }
        
        public void DispelStatusEffect(UnitEntityController target, DispelStatusEffectTarget targetEffect)
        {
            var sToRemove = new List<StatusEffectApplicationData>();
            switch (targetEffect)
            {
                //TODO Handle dispel logic
                case DispelStatusEffectTarget.Positive:
                    foreach (var applicationData in _statusEffectsContainer[target])
                    {
                        if (applicationData.StatusEffectApplied.isDispelable && applicationData.StatusEffectApplied.isPositive)
                        {
                            sToRemove.Add(applicationData);
                        }
                    }
                    break;
                case DispelStatusEffectTarget.Negative:
                    foreach (var applicationData in _statusEffectsContainer[target])
                    {
                        if (applicationData.StatusEffectApplied.isDispelable && !applicationData.StatusEffectApplied.isPositive)
                        {
                            sToRemove.Add(applicationData);
                        }
                    }
                    break;
                case DispelStatusEffectTarget.All:
                    foreach (var applicationData in _statusEffectsContainer[target])
                    {
                        if (applicationData.StatusEffectApplied.isDispelable)
                        {
                            sToRemove.Add(applicationData);
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(targetEffect), targetEffect, null);
            }
            _statusEffectsContainer[target].RemoveAll(effect => sToRemove.Contains(effect));
        }
    }
}