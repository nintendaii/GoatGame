using Abilities;
using Signals;
using Zenject;

namespace Commands
{
    public class DeactivatePassiveAbilitiesCommand: ICommandWithParameters
    {
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;
        
        public void Execute(ISignal signal)
        {
            var param = (DeactivatePassiveAbilitiesSignal)signal;
            _statusEffectSystem.RemoveAbilityPassiveEffects(param.PassiveAbility);
        }
    }
}