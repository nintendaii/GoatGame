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
    }
}