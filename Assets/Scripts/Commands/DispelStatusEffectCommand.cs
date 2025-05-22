using Signals;

namespace Commands
{
    public class DispelStatusEffectCommand: ICommandWithParameters
    {
        public void Execute(ISignal signal)
        {
            var param = (DispelStatusEffectSignal)signal;
            foreach (var t in param.EffectProcessorData.Targets)
            {
                t.DispelStatusEffect(param.AbilityEffectData.dispelStatusEffectTarget);
            }
        }
    }
}