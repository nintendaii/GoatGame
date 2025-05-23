using Abilities;
using Data.Abilities;
using Signals;
using Zenject;

namespace Commands
{
    public class DispelStatusEffectCommand: ICommandWithParameters
    {
        [Inject] private readonly StatusEffectSystem _statusEffectSystem;
        public void Execute(ISignal signal)
        {
            var param = (DispelStatusEffectSignal)signal;
            foreach (var t in param.EffectProcessorData.Targets)
            {
                _statusEffectSystem.DispelStatusEffect(t, param.AbilityEffectData.dispelStatusEffectTarget);
            }
        }
    }
}