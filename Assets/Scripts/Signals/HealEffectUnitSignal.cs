using Abilities;
using Data.Abilities;

namespace Signals
{
    public class HealEffectUnitSignal: EffectUnitSignalBase
    {
        public float Value;
        public HealEffectUnitSignal(EffectProcessorData effectProcessorData, float v) : base(effectProcessorData)
        {
            Value = v;
        }
    }
}