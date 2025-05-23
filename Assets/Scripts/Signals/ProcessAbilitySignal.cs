using System.Collections.Generic;
using Abilities;
using SO;
using Unit;

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