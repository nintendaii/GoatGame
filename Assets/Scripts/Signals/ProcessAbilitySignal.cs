using SO;

namespace Signals
{
    public class ProcessAbilitySignal: ISignal
    {
        public SOAbilityData AbilityData;
        public ProcessAbilitySignal(SOAbilityData abilityData)
        {
            AbilityData = abilityData;
        }
    }
}