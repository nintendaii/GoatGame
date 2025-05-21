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
            var effectProcessorData = new EffectProcessorData
            {
                Ability = param.AbilityOwnerData.AbilityData,
                Source = param.AbilityOwnerData.UnitEntityController,
                Targets = param.UnitTargets
            };
            _abilityProcessorSystem.UseAbility(effectProcessorData);
            _abilityCooldownSystem.SendAbilityOnCooldown(param.AbilityOwnerData);
        }
    }
}