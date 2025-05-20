using Abilities;
using Signals;
using Zenject;

namespace Commands
{
    public class UseAbilityCommand: ICommandWithParameters
    {
        [Inject] private readonly AbilityProcessorSystem _abilityProcessorSystem;
        [Inject] private readonly AbilityCooldownSystem _abilityCooldownSystem;
        
        public void Execute(ISignal signal)
        {
            var param = (UseAbilitySignal)signal;
            _abilityProcessorSystem.UseAbility(param.AbilityOwnerData.UnitEntityController, param.AbilityOwnerData.AbilityData, param.UnitTargets);
            _abilityCooldownSystem.SendAbilityOnCooldown(param.AbilityOwnerData);
        }
    }
}