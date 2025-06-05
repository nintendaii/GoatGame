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
        public UnitCoreStats unitMaxCoreStats = new();
        private List<StatusEffect> _appliedStatusEffects = new();

        public bool IsAlive => unitEntityData.CoreStats.Health.Value > 0;

        private void Awake()
        {
            unitEntityData = UnitDataSO.Clone();
            unitAvatarSprite = UnitDataSO.unitAvatarSprite;
            unitMaxCoreStats = unitEntityData.CoreStats.Clone();
            foreach (var a in UnitDataSO.abilities)
            {
                AbilitiesRuntime.Add(new AbilityRuntimeData
                {
                    AbilityData = a,
                    IsReady = true
                });
            }
        }

        public void InitPassiveAbilities()
        {
            foreach (var a in AbilitiesRuntime)
            {
                if (a.AbilityData.abilityType is AbilityType.Passive or AbilityType.Aura)
                {
                    _signalBus.Fire(new ProcessAbilitySignal(a.AbilityData, this));
                }
            }
        }

        private void DeactivatePassiveAbilities()
        {
            foreach (var a in AbilitiesRuntime)
            {
                if (a.AbilityData.abilityType is AbilityType.Passive or AbilityType.Aura)
                {
                    _signalBus.Fire(new DeactivatePassiveAbilitiesSignal(this, a.AbilityData));
                }
            }
        }

        private void ManipulateHealth(float value)
        {
            Debug.Log(value < 0 ? $"Deal {value} damage" : $"Restored {value} health");
            var sum = value + unitEntityData.CoreStats.Health.Value;
            if (sum>unitMaxCoreStats.Health.Value)
            {
                unitMaxCoreStats.Health.Value = sum;
            }
            unitEntityData.CoreStats.Health.Value += value;
            if (unitEntityData.CoreStats.Health.Value<=0)
            {
                HandleDeath();
            }
        }

        private void HandleDeath()
        {
            Debug.Log("Dead");
            DeactivatePassiveAbilities();
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
            if (statType==StatType.Health)
            {
                var sum = unitEntityData.CoreStats.Health.Value + value;
                if (IsAlive && sum<=0)
                {
                    unitEntityData.CoreStats.Health.Value = 1;
                    return;
                }

                ManipulateHealth(value);
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
            InitPassiveAbilities();
            Heal(value);
        }

        public void Kill()
        {
            DealDamage(unitEntityData.CoreStats.Health.Value);
        }
    }
}