using System.Linq;
using Combat;
using Formulas;
using Signals;
using Turn;
using UI;
using UnityEngine;
using Zenject;

namespace Commands
{
    public class DealAttackDamageUnitCommand: ICommandWithParameters
    {
        [Inject] private readonly CombatManager _combatManager;
        [Inject] private readonly UnitItemsUIContainer _unitItemsUIContainer;
        [Inject] private readonly TurnManager _turnManager;
        [Inject] private readonly SignalBus _signalBus;

        public void Execute(ISignal signal)
        {
            var param = (DealAttackDamageUnitSignal)signal;
            
            if (param.Targets.Contains(param.Source))
            {
                Debug.LogWarning("Cant attack self");
                return;
            }

            var filteredList = param.Targets.Where(x => x.IsAlive).ToList();

            if (filteredList.Count==0)
            {
                return;
            }
            foreach (var u in filteredList)
            {
                var damage = DamageCalculator.CalculateAttackFinalDamage(param.Source.unitEntityData,
                    u.unitEntityData);
                u.DealDamage(damage);
                _unitItemsUIContainer.UpdateHealth(u.unitEntityData.CoreStats.Health.Value,u.unitEntityData.Id);
                if (!u.IsAlive)
                {
                    _turnManager.RemoveUnitFromQueue(u);
                }
            }
            
            _signalBus.Fire(new AdvanceNextTurnSignal());
        }
    }
}