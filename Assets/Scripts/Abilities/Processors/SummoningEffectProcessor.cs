using Data.Abilities;
using Signals;
using Zenject;

namespace Abilities.Processors
{
    public class SummoningEffectProcessor: IAbilityEffectProcessor
    {
        [Inject] private readonly SignalBus _signalBus;
        public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            if (effectData.unitsSummon!=null)
            {
                _signalBus.Fire(new SummonUnitEffectSignal(effectProcessorData, effectData));
            }
        }
    }
}