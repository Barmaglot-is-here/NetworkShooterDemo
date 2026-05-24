using System;

namespace Signals
{
    public interface IReadySignal
    {
        void Subscribe(Action action);
    }
}