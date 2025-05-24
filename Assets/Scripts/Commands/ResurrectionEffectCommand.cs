using System.Linq;
using Signals;
using Turn;
using UI;
using Zenject;

namespace Commands
{
    public class ResurrectionEffectCommand: ICommandWithParameters
    {
        [Inject] private readonly SignalBus _signalBus;
        [Inject] private readonly UnitItemsUIContainer _unitItemsUIContainer;
        [Inject] private readonly TurnManager _turnManager;

        public void Execute(ISignal signal)
        {
            var param = (ResurrectionEffectSignal)signal;
            var filteredList = param.EffectProcessorData.Targets.Where(x => !x.IsAlive).ToList();
            if (filteredList.Count==0)
            {
                return;
            }
            foreach (var u in filteredList)
            {
                u.Resurrect(param.AbilityEffectData.value);
                _unitItemsUIContainer.UpdateHealth(u.unitEntityData.CoreStats.Health.Value,u.unitEntityData.Id);
                _turnManager.AddUnitToQueue(u);
            }
            
            _signalBus.Fire(new AdvanceNextTurnSignal());
        }
    }
}