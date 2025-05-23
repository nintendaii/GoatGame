using System;
using System.Collections.Generic;
using Abilities;
using Data.Abilities;
using Data.General;
using Data.Stats;
using NUnit.Framework;
using Signals;
using SO;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class UnitEntityController : MonoBehaviour
    {
        public UnitEntityData unitEntityData;
        [NonSerialized] public Sprite unitAvatarSprite;
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;
        [Inject] private readonly SignalBus _signalBus;
        public SOUnitData UnitData;
        public List<AbilityRuntimeData> AbilitiesRuntime = new();
        public UnitTeam UnitTeam;

        private List<StatusEffect> _appliedStatusEffects = new();

        public bool IsAlive => unitEntityData.CoreStats.Health.Value >= 0;

        private void Awake()
        {
            unitEntityData = UnitData.Clone();
            unitAvatarSprite = UnitData.unitAvatarSprite;
            foreach (var a in UnitData.abilities)
            {
                AbilitiesRuntime.Add(new AbilityRuntimeData
                {
                    AbilityData = a,
                    IsReady = true
                });
            }
        }

        public void DealDamage(float damage)
        {
            
            Debug.Log($"Dealed {damage}");
            unitEntityData.CoreStats.Health.Value -= damage;
        }

        public void Heal(float value)
        {
            unitEntityData.CoreStats.Health.Value += value;
        }

        public void ApplyStatusEffect(StatusEffect statusEffect)
        {
            _statusEffectSystem.ApplyStatusEffect(this, statusEffect);
            _appliedStatusEffects.Add(statusEffect);
        }

        public void RemoveStatusEffect(StatusEffect statusEffect)
        {
            //This is called at the beginning of turn on which effect stopped its iteration
            _appliedStatusEffects.Remove(statusEffect);
        }

        public void DispelStatusEffect(DispelStatusEffectTarget target)
        {
            _statusEffectSystem.DispelStatusEffect(this, target);
        }

        public void ManipulateStat(StatType statType, float value)
        {
            unitEntityData.GetAllStats()[statType] += value;
        }

        public void UseAbility(SOAbilityData abilityData, List<UnitEntityController> targets)
        {
            var ab = AbilitiesRuntime.Find(x => x.AbilityData == abilityData);
            if (ab!=null)
            {
                if (!ab.IsReady)
                {
                    Debug.Log($"Ability {abilityData.abilityName} is not ready");
                    return;
                }
                _signalBus.Fire(new UseAbilitySignal(this, abilityData, targets));
            }
        }

        public void CooldownAbility(SOAbilityData abilityData)
        {
            var ab = AbilitiesRuntime.Find(x => x.AbilityData == abilityData);
            if (ab!=null)
            {
                if (!ab.IsReady)
                {
                    ab.IsReady = true;
                }
            }
        }

        public void Resurrect()
        {
            //TODO implement this
        }

        public void Kill()
        {
            //TODO implement this
        }
    }
}