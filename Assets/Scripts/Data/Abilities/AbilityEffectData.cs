using System.Collections.Generic;
using Data.Dmage;
using Data.Stats;
using SO;

namespace Data.Abilities
{
    [System.Serializable]
    public class AbilityEffectData
    {
        public AbilityEffect effectType;
        public AbilityEffectData delayedEffectType; //for delayed effect
        public EffectDuration effectDuration;
        public float value;
        public DamageType damageType; //for damage effect
        public ElementalDamage elementalDamage = new ElementalDamage
        {
            Fire = new StatBase(StatType.FireDamage,0),
            Ice = new StatBase(StatType.IceDamage,0),
            Dark = new StatBase(StatType.DarkDamage,0),
            Lightning = new StatBase(StatType.LightningDamage,0),
        }; //for damage effect
        public int duration; // only relevant for continuous effects
        public StatType statAffected; // relevant for buffs
        public List<SOUnitData> unitsSummon; //for summoning
        public bool isDispelable;
        public DispelStatusEffectTarget dispelStatusEffectTarget;
        public bool isPositive;
    }
}