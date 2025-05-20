using System;
using System.Collections.Generic;
using Combat;
using Data.Abilities;
using Data.Stats;
using SO;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Unit
{
    public class UnitEntityController : MonoBehaviour
    {
        [NonSerialized] public UnitEntityData unitEntityData;
        [NonSerialized] public Sprite unitAvatarSprite;
        [Inject] private readonly CombatManager _combatManager;
        public SOUnitData UnitData;

        public bool IsAlive => unitEntityData.CoreStats.Health.Value >= 0;

        private void Awake()
        {
            unitEntityData = UnitData.Clone();
            unitAvatarSprite = UnitData.unitAvatarSprite;
        }

        public void DealDamage(float damage)
        {
            unitEntityData.CoreStats.Health.Value -= damage;
        }

        public void Heal(float value)
        {
            unitEntityData.CoreStats.Health.Value += value;
        }

        public void ApplyStatusEffect(StatusEffect statusEffect)
        {
            //TODO send event to StatusEffectController
        }
        

        public void DispelStatusEffect(DispelStatusEffectTarget target)
        {
            //TODO send event to StatusEffectController
        }

        public void ManipulateStat(StatType statType, float value)
        {
            unitEntityData.GetAllStats()[statType] += value;
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