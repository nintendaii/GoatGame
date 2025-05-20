using Data.Stats;

namespace Data.Abilities
{
    public class StatusEffect
    {
        public AbilityEffect effectType;
        public AbilityEffectData delayedEffect; //for delayed effects
        public int duration;
        public bool isDispelable;
        public bool isPositive;
        public float value; //for stat buff/debuff
        public StatType statAffected; //for stat buff/debuff
    }
}