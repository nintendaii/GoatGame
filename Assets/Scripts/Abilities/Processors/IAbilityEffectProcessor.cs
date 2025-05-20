using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities.Processors
{
    public interface IAbilityEffectProcessor
    {
        void Apply(UnitEntityController source, AbilityEffectData effectData, List<UnitEntityController> targets = null);
    }
}