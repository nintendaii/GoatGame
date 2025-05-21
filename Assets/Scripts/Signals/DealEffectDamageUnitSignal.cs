using System.Collections.Generic;
using Abilities;
using Unit;

namespace Signals
{
    public class DealEffectDamageUnitSignal: ISignal
    {
        public EffectProcessorData EffectProcessorData;
        public Dictionary<UnitEntityController, float> DamageDictionary;

        public DealEffectDamageUnitSignal(EffectProcessorData effectProcessorData, Dictionary<UnitEntityController, float> damageDictionary)
        {
            DamageDictionary = damageDictionary;
            EffectProcessorData = effectProcessorData;
        }
    }
}