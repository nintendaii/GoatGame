using System.Collections.Generic;
using Data.Abilities;
using Unit;

namespace Abilities.Processors
{
    public class HealingEffectProcessor: IAbilityEffectProcessor
    {
        public void Apply(UnitEntityController source, AbilityEffectData effectData, List<UnitEntityController> targets = null)
        {
            var value = effectData.value;
            if (targets != null)
                foreach (var t in targets)
                {
                    t.Heal(value);
                }
        }
    }
}