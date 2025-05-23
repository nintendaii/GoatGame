using Data.Abilities;
using Signals;
using UnityEngine;
using Zenject;

namespace Abilities.Processors
{
    public class StunEffectProcessor: IAbilityEffectProcessor
    {
        [Inject] private readonly SignalBus _signalBus;
        public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            var targets = effectProcessorData.Targets;
            if (targets!=null)
            {
                _signalBus.Fire(new StunStatusEffectSignal(effectProcessorData, effectData));
            }
            else
            {
                Debug.LogError("Targets are empty");
            }
        }
    }
}