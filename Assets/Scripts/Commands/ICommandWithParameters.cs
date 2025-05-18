using Signals;

namespace Commands
{
    public interface ICommandWithParameters
    {
        void Execute(ISignal signal);
    }
}