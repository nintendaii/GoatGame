using Combat;
using Data.General;
using Signals;
using Turn;
using UI;
using Unit;
using Zenject;

namespace Commands
{
    public class SummonUnitEffectCommand: ICommandWithParameters
    {
        [Inject] private readonly UnitsContainer _unitsContainer;
        [Inject] private readonly TurnManager _turnManager;
        [Inject] private readonly UnitItemsUIContainer _unitItemsUIContainer;
        [Inject] private readonly UnitsTurnOrderScreen _unitsTurnOrderScreen;

        public void Execute(ISignal signal)
        {
            var param = (SummonUnitEffectSignal)signal;
            var effectData = param.AbilityEffectData;
            foreach (var u in effectData.unitsSummon)
            {
                var spawnedUnit = _unitsContainer.SpawnUnit(u);
                spawnedUnit.UnitTeam = effectData.isAlly
                    ? param.EffectProcessorData.Source.UnitTeam
                    : param.EffectProcessorData.Source.UnitTeam == UnitTeam.TeamA
                        ? UnitTeam.TeamB
                        : UnitTeam.TeamA;
                _turnManager.AddUnitToQueue(spawnedUnit);
                //Temp UI stuff
                _unitItemsUIContainer.AddUnit(spawnedUnit,_unitsContainer.UnitEntityContainer.IndexOf(spawnedUnit));
            }

            //_unitsTurnOrderScreen.GenerateOrder();
        }
    }
}