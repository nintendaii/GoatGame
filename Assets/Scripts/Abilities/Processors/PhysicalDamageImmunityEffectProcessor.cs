using Data.Abilities;
using Signals;
using Zenject;

namespace Abilities.Processors
{
    public class PhysicalDamageImmunityEffectProcessor: IAbilityEffectProcessor
    {
        [Inject] private readonly SignalBus _signalBus;
        public void Apply(EffectProcessorData effectProcessorData, AbilityEffectData effectData)
        {
            var targets = effectProcessorData.Targets;
            if (targets!=null)
            {
                _signalBus.Fire(new PhysicalDamageImmunityStatusEffectSignal(effectProcessorData, effectData));
            }
        }
    }
}