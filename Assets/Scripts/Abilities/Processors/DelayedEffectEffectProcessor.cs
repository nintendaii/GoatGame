using Data.Abilities;
using Signals;
using Zenject;

namespace Abilities.Processors
{
    public class DelayedEffectEffectProcessor: IAbilityEffectProcessor
    {
        [Inject] private readonly SignalBus _signalBus;
        public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            if (effectProcessorData.Targets!=null)
            {
                _signalBus.Fire(new DelayedEffectStatusEffectSignal(effectProcessorData, effectData));
            }
        }
    }
}