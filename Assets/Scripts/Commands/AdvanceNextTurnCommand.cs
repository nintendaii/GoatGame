using Abilities;
using Combat;
using Signals;
using Turn;
using UI;
using UnityEngine;
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
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;
        [Inject] private readonly SignalBus _signalBus;

        public void Execute()
        {
            _unitsTurnOrderScreen.GenerateOrder();
            var unit = _turnManager.ExecuteTurn();
            Debug.Log($"Unit moves {unit.UnitEntityController.name}");
            _abilityCooldownSystem.CheckAbilitiesCooldown(_turnManager.CurrentTurn);
            _combatManager.currentTurnEntity = unit.UnitEntityController;
            var isUnitAllowToMove = _statusEffectSystem.ValidateStatusEffects(unit.UnitEntityController);
            if (isUnitAllowToMove)
            {
                _unitControlsScreen.SetUnitName(unit.UnitEntityController.unitEntityData.Name);
                _unitControlsScreen.SetAvatar(unit.UnitEntityController.unitAvatarSprite);
                _unitControlsScreen.SetAbilities(unit.UnitEntityController.AbilitiesRuntime);
            }
            else
            {
                _signalBus.Fire(new AdvanceNextTurnSignal());
            }
        }
    }
}