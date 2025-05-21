using Data.Abilities;
using Signals;
using Zenject;

namespace Abilities.Processors
{
    public class HealingEffectProcessor: IAbilityEffectProcessor
    {
        [Inject] private readonly SignalBus _signalBus;
         public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            var value = effectData.value;
            var targets = effectProcessorData.Targets;
            if (targets != null)
                _signalBus.Fire(new HealEffectUnitSignal(effectProcessorData, value));
        }
    }
}