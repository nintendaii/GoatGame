using SO;
using Unit;

namespace Signals
{
    public class ProcessAbilitySignal: ISignal
    {
        public SOAbilityData AbilityData;
        public UnitEntityController Source;
        public ProcessAbilitySignal(SOAbilityData abilityData, UnitEntityController source=null)
        {
            AbilityData = abilityData;
            Source = source;
        }
    }
}