using System.Collections.Generic;
using Unit;

namespace Signals
{
    public class DealAttackDamageUnitSignal: ISignal
    {
        public UnitEntityController Source;
        public List<UnitEntityController> Targets;

        public DealAttackDamageUnitSignal(UnitEntityController source, List<UnitEntityController> targets)
        {
            Source = source;
            Targets = targets;
        }
    }
}