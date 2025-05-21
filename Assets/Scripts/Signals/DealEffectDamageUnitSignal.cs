using System.Collections.Generic;
using Unit;

namespace Signals
{
    public class DealEffectDamageUnitSignal: ISignal
    {
        public UnitEntityController Source;
        public List<UnitEntityController> Targets;
        public float Value;

        public DealEffectDamageUnitSignal(UnitEntityController source, List<UnitEntityController> targets, float value)
        {
            Source = source;
            Targets = targets;
            Value = value;
        }
    }
}