using System;
using System.Collections.Generic;
using Data.Abilities;
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
            _statusEffectsContainer[target].Add(new StatusEffectApplicationData{StatusEffectApplied = effect, ApplicationTurn = currentTurn});
        }

        public void CheckStatusEffects()
        {
            
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

    public class StatusEffectApplicationData
    {
        public StatusEffect StatusEffectApplied;
        public int ApplicationTurn;
    }
}