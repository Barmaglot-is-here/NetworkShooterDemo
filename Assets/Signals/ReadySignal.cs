using System;

namespace Signals
{
    public class ReadySignal : IReadySignal
    {
        private event Action _onReady;

        private bool _isReady;

        public void SetReady()
        {
            if (_isReady)
                throw new Exception("Signal marked ready twice");

            _onReady?.Invoke();

            _onReady = null;

            _isReady = true;
        }

        public void Subscribe(Action action)
        {
            if (_isReady)
            {
                action.Invoke();

                return;
            }

            _onReady += action;
        }
    }
}
