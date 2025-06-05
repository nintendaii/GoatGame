using SO;
using Unit;

namespace Signals
{
    public class DeactivatePassiveAbilitiesSignal: ISignal
    {
        public UnitEntityController Source;
        public SOAbilityData PassiveAbility;

        public DeactivatePassiveAbilitiesSignal(UnitEntityController source, SOAbilityData abilityData)
        {
            Source = source;
            PassiveAbility = abilityData;
        }
    }
}