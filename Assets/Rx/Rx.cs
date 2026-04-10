using System;

namespace Assets.Rx
{
    public class Rx<T> : IReadOnlyRx<T>
    {
        private T _value;
        public T Value
        {
            get => _value;
            set
            {
                _value = value;

                Changed?.Invoke(value);
            }
        }

        public event Action<T> Changed;

        public Rx(T value = default) => SetWithoutNotify(value);
        public void SetWithoutNotify(T value) => _value = value;

        public override string ToString() => _value.ToString();
        public override int GetHashCode() => _value.GetHashCode();
    }
}
