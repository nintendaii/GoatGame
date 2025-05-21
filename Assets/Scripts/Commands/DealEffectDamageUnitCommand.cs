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
    public class DealEffectDamageUnitCommand: ICommandWithParameters
    {
        [Inject] private readonly CombatManager _combatManager;
        [Inject] private readonly UnitItemsUIContainer _unitItemsUIContainer;
        [Inject] private readonly TurnManager _turnManager;
        [Inject] private readonly SignalBus _signalBus;
        public void Execute(ISignal signal)
        {
            var param = (DealEffectDamageUnitSignal)signal;
            
            var filteredList = param.Targets.Where(x => x.IsAlive).ToList();
            if (filteredList.Count==0)
            {
                return;
            }
            foreach (var u in filteredList)
            {
                
                u.DealDamage(param.Value);
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