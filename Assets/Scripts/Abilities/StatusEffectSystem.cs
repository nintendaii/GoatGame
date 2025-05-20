using System;
using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities
{
    public class StatusEffectSystem
    {
        private Dictionary<UnitEntityController, List<StatusEffect>> _statusEffectsContainer = new();

        public void ApplyStatusEffect(UnitEntityController target, StatusEffect effect)
        {
            _statusEffectsContainer[target].Add(effect);
        }
        
        public void DispelStatusEffect(UnitEntityController target, DispelStatusEffectTarget targetEffect)
        {
            var sToRemove = new List<StatusEffect>();
            switch (targetEffect)
            {
                //TODO Handle dispel logic
                case DispelStatusEffectTarget.Positive:
                    foreach (var e in _statusEffectsContainer[target])
                    {
                        if (e.isDispelable && e.isPositive)
                        {
                            sToRemove.Add(e);
                        }
                    }
                    break;
                case DispelStatusEffectTarget.Negative:
                    foreach (var e in _statusEffectsContainer[target])
                    {
                        if (e.isDispelable && !e.isPositive)
                        {
                            sToRemove.Add(e);
                        }
                    }
                    break;
                case DispelStatusEffectTarget.All:
                    foreach (var e in _statusEffectsContainer[target])
                    {
                        if (e.isDispelable)
                        {
                            sToRemove.Add(e);
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