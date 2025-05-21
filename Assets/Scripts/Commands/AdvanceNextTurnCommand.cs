using Abilities;
using Combat;
using Turn;
using UI;
using Zenject;

namespace Commands
{
    public class AdvanceNextTurnCommand: ICommand
    {
        [Inject] private readonly TurnManager _turnManager;
        [Inject] private readonly AbilityCooldownSystem _abilityCooldownSystem;
        [Inject] private readonly UnitsTurnOrderScreen _unitsTurnOrderScreen;
        [Inject] private readonly UnitControlsScreen _unitControlsScreen;
        [Inject] private readonly CombatManager _combatManager;
        

        public void Execute()
        {
            _unitsTurnOrderScreen.GenerateOrder();
            var unit = _turnManager.ExecuteTurn();
            _combatManager.currentTurnEntity = unit.UnitEntityController;
            _unitControlsScreen.SetUnitName(unit.UnitEntityController.unitEntityData.Name);
            _unitControlsScreen.SetAvatar(unit.UnitEntityController.unitAvatarSprite);
            _unitControlsScreen.SetAbilities(unit.UnitEntityController.AbilitiesRuntime);
            _abilityCooldownSystem.CheckAbilitiesCooldown(_turnManager.currentTurn);
        }
    }
}