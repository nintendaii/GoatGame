using System.Collections.Generic;
using Data.Abilities;
using Signals;
using Unit;
using Zenject;

namespace Abilities.Processors
{
    public class DisarmEffectProcessor: IAbilityEffectProcessor
    {
        [Inject] private readonly SignalBus _signalBus;
        public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            if (effectProcessorData.Targets!=null)
            {
                _signalBus.Fire(new DisarmStatusEffectSignal(effectProcessorData, effectData));
            }
        }
    }
}