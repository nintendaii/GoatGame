using System.Collections.Generic;
using Data.Dmage;
using Data.Stats;
using SO;
using Unit;
using UnityEngine;

namespace Data.Abilities
{
    [System.Serializable]
    public class AbilityEffectData
    {
        public AbilityEffect effectType;
        public float value;
        [Header("Damage")]
        public DamageType damageType; //for damage effect
        public ElementalDamage elementalDamage = new()
        {
            Fire = new StatBase(StatType.FireDamage,0),
            Ice = new StatBase(StatType.IceDamage,0),
            Dark = new StatBase(StatType.DarkDamage,0),
            Lightning = new StatBase(StatType.LightningDamage,0),
        }; //for damage effect
        [Header("Buff/debuff")]
        public EffectDuration effectDuration;
        public StatType statAffected; // relevant for buffs
        [Header("Summoning")] 
        public bool isAlly;
        public List<UnitEntityController> unitsSummon; //for summoning
        [Header("Status effect")]
        public int duration; // only relevant for continuous effects
        public bool isDispelable;
        public DispelStatusEffectTarget dispelStatusEffectTarget;
        public StatusEffectIterationType IterationType;
        public bool isPositive;
        [Header("Delayed effect")] 
        public bool hasDelayedEffect; //for delayed effect
        public AbilityEffect delayedEffectType;
        
        public AbilityEffectData Clone()
        {
            return new AbilityEffectData
            {
                effectType = this.effectType,
                value = this.value,
                damageType = this.damageType,
                elementalDamage = new ElementalDamage
                {
                    Fire = this.elementalDamage.Fire.Clone(),
                    Ice = this.elementalDamage.Ice.Clone(),
                    Dark = this.elementalDamage.Dark.Clone(),
                    Lightning = this.elementalDamage.Lightning.Clone()
                },
                effectDuration = this.effectDuration,
                statAffected = this.statAffected,
                isAlly = this.isAlly,
                unitsSummon = this.unitsSummon != null ? new List<UnitEntityController>(this.unitsSummon) : null,
                duration = this.duration,
                isDispelable = this.isDispelable,
                dispelStatusEffectTarget = this.dispelStatusEffectTarget,
                IterationType = this.IterationType,
                isPositive = this.isPositive,
                hasDelayedEffect = this.hasDelayedEffect,
                delayedEffectType = this.delayedEffectType
            };
        }
    }
}