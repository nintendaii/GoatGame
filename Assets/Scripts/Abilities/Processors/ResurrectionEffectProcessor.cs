using System;
using System.Collections.Generic;
using Data.Abilities;
using Signals;
using Unit;
using Zenject;

namespace Abilities.Processors
{
    public class ResurrectionEffectProcessor: IAbilityEffectProcessor
    {
        [Inject] private readonly SignalBus _signalBus;
        public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            var targets = effectProcessorData.Targets;
            if (targets!=null)
            {
                _signalBus.Fire(new ResurrectionEffectSignal(effectProcessorData, effectData));
            }
        }
    }
}