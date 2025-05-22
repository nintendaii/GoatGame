using System.Collections.Generic;
using SO;
using Unit;

namespace Data.Abilities
{
    public class EffectProcessorData
    {
        public UnitEntityController Source;
        public SOAbilityData Ability;
        public List<UnitEntityController> Targets;
    }
}