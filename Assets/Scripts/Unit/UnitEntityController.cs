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
        public SOUnitData UnitDataSO;
        public List<AbilityRuntimeData> AbilitiesRuntime = new();
        public UnitTeam UnitTeam;

        private List<StatusEffect> _appliedStatusEffects = new();

        public bool IsAlive => unitEntityData.CoreStats.Health.Value > 0;

        private void Awake()
        {
            unitEntityData = UnitDataSO.Clone();
            unitAvatarSprite = UnitDataSO.unitAvatarSprite;
            foreach (var a in UnitDataSO.abilities)
            {
                AbilitiesRuntime.Add(new AbilityRuntimeData
                {
                    AbilityData = a,
                    IsReady = true
                });
            }
        }

        private void ManipulateHealth(float value)
        {
            Debug.Log(value < 0 ? $"Deal {value} damage" : $"Restored {value} health");
            unitEntityData.CoreStats.Health.Value += value;
        }

        public void Heal(float value)
        {
            ManipulateHealth(value);
        }

        public void DealDamage(float value)
        {
            ManipulateHealth(-value);
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

        public void ManipulateStat(StatType statType, float value)
        {
            if (statType==StatType.Health && IsAlive && unitEntityData.CoreStats.Health.Value-value<=0)
            {
                unitEntityData.CoreStats.Health.Value = 1;
                return;
            }
            //rework this
            unitEntityData.GetAllStats()[statType].Value += value;
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

        public void Resurrect(float value)
        {
            Heal(value);
        }

        public void Kill()
        {
            DealDamage(unitEntityData.CoreStats.Health.Value);
        }
    }
}