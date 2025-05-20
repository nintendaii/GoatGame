using System.Collections.Generic;
using Abilities;
using SO;
using Unit;

namespace Signals
{
    public class UseAbilitySignal: ISignal
    {
        public AbilityOwnerData AbilityOwnerData;

        public List<UnitEntityController> UnitTargets;
        public UseAbilitySignal(UnitEntityController unit, SOAbilityData abilityData, List<UnitEntityController> unitTargets)
        {
            var aOd = new AbilityOwnerData
            {
                AbilityData = abilityData,
                UnitEntityController = unit
            };
            AbilityOwnerData = aOd;
            UnitTargets = unitTargets;
        }
    }
}