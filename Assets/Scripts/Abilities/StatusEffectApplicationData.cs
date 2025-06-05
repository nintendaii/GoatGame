using System;
using Data.Abilities;
using SO;

namespace Abilities
{
    public class StatusEffectApplicationData
    {
        public SOAbilityData OriginAbility;
        public StatusEffect StatusEffectApplied;
        public int ApplicationTurn;
        public int StatusEffectIteration;
        public Action OnEffectExpired;
        public Action OnEffectActivated;
    }
}