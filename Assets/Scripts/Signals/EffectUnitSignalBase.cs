using Data.Abilities;

namespace Signals
{
    public abstract class EffectUnitSignalBase: ISignal
    {
        public EffectProcessorData EffectProcessorData;

        protected EffectUnitSignalBase(EffectProcessorData effectProcessorData)
        {
            EffectProcessorData = effectProcessorData;
        }
    }
}