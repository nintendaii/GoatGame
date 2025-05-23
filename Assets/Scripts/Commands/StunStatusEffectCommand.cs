using Data.Abilities;
using ModestTree;
using Signals;
using UnityEngine;

namespace Commands
{
    public class StunStatusEffectCommand: ICommandWithParameters
    {
        public void Execute(ISignal signal)
        {
            var param = (StunStatusEffectSignal)signal;
            var effectData = param.AbilityEffectData;
            var stunEffect = new StatusEffect
            {
                effectType = AbilityEffect.Stun,
                isDispelable = effectData.isDispelable,
                isPositive = effectData.isPositive,
                duration = effectData.duration,
                iterationType = effectData.IterationType
            };
            Debug.Log("In stunn");
            foreach (var t in param.EffectProcessorData.Targets)
            {
                Debug.Log($"Applied to {t.name}");
                t.ApplyStatusEffect(stunEffect);
            }
        }
    }
}