using Unity.Netcode;

namespace Assets.Game.Scripts.Gameplay
{
    public class HealthComponent : NetworkBehaviour
    {
        private NetworkVariable<int> _value;

        public NetworkVariable<int>.OnValueChangedDelegate OnChanged
        {
            get => _value.OnValueChanged; 
            set => _value.OnValueChanged = value;
        }
        public int Value => _value.Value;

        private void Awake()
        {
            _value = new(100);
        }

        [Rpc(SendTo.Server)]
        public void ApplyDamageRpc(int value)
        {
            _value.Value -= value;

            if (_value.Value < 0)
                _value.Value = 0;
        }
    }
}
