using System.Collections.Generic;
using Combat;
using Data.Abilities;
using Unit;
using Zenject;

namespace Abilities.Processors
{
    public class SummoningEffectProcessor: IAbilityEffectProcessor
    {
        [Inject] private readonly CombatManager _combatManager;
        public void Apply(UnitEntityController source, AbilityEffectData effectData, List<UnitEntityController> targets = null)
        {
            if (effectData.unitsSummon!=null)
            {
                foreach (var u in effectData.unitsSummon)
                {
                    _combatManager.AddUnit(u);
                }
            }
        }
    }
}