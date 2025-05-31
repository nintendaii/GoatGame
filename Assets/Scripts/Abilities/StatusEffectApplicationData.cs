using System;
using Data.Abilities;

namespace Abilities
{
    public class StatusEffectApplicationData
    {
        public StatusEffect StatusEffectApplied;
        public int ApplicationTurn;
        public int StatusEffectIteration;
        public Action OnEffectExpired;
        public Action OnEffectActivated;
    }
}