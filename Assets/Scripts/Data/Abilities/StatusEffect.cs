using Data.Stats;
using SO;

namespace Data.Abilities
{
    public class StatusEffect
    {
        public SOAbilityData originAbility;
        public AbilityEffect effectType;
        public AbilityEffectData delayedEffect; //for delayed effects
        public int duration;
        public bool isPermanent;
        public bool isDispelable;
        public bool isPositive;
        public StatusEffectIterationType iterationType;
        public float value; //for stat buff/debuff
        public StatType statAffected; //for stat buff/debuff
    }
}