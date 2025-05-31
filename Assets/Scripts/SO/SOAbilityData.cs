using System.Collections.Generic;
using Data.Abilities;
using UnityEngine;

namespace SO
{
    [CreateAssetMenu(fileName = "SOAbilityData", menuName = "SO/Ability")]
    public class SOAbilityData: ScriptableObject
    {
        [Header("Metadata")]
        public string abilityName;
        public string description;
        public Sprite icon;
        public AbilityType abilityType;
        public AbilityTarget targetType;
        public int cooldown;
        
        [Header("Effects")]
        public List<AbilityEffectData> effects;
        
        public SOAbilityData Clone()
        {
            var copy = CreateInstance<SOAbilityData>();

            copy.abilityName = this.abilityName;
            copy.description = this.description;
            copy.icon = this.icon;
            copy.abilityType = this.abilityType;
            copy.targetType = this.targetType;
            copy.cooldown = this.cooldown;

            copy.effects = new List<AbilityEffectData>();
            foreach (var effect in this.effects)
            {
                copy.effects.Add(effect.Clone());
            }

            return copy;
        }
    }
}