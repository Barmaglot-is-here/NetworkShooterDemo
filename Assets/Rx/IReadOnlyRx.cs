using System;

namespace Assets.Rx
{
    public interface IReadOnlyRx<T>
    {
        T Value { get; }

        event Action<T> Changed;
    }
}