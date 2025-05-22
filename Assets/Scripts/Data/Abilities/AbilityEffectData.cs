using System.Collections.Generic;
using Data.Dmage;
using Data.Stats;
using SO;
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
        public ElementalDamage elementalDamage = new ElementalDamage
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
        public List<SOUnitData> unitsSummon; //for summoning
        [Header("Status effect")]
        public int duration; // only relevant for continuous effects
        public bool isDispelable;
        public DispelStatusEffectTarget dispelStatusEffectTarget;
        public StatusEffectIterationType IterationType;
        public bool isPositive;
        [Header("Delayed effect")]
        public AbilityEffectData delayedEffectType; //for delayed effect
    }
}