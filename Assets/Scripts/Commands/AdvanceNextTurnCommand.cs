using Abilities;
using Turn;
using Zenject;

namespace Commands
{
    public class AdvanceNextTurnCommand: ICommand
    {
        [Inject] private readonly TurnManager _turnManager;
        [Inject] private readonly AbilityCooldownSystem _abilityCooldownSystem;
        public void Execute()
        {
            _abilityCooldownSystem.CheckAbilitiesCooldown(_turnManager.currentTurn);
        }
    }
}