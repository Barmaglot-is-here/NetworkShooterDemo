using Unity.Netcode;

namespace Assets.Game.Scripts.Level
{
    public class TeamBaseDoor : NetworkBehaviour
    {
        public NetworkVariable<DoorState> State;

        private void Awake()
        {
            State = new NetworkVariable<DoorState>(DoorState.Closed);
        }

        public override void OnNetworkSpawn()
        {
            State.OnValueChanged += OnStateChanged;
        }

        private void OnStateChanged(DoorState previousValue, DoorState newValue)
        {
            gameObject.SetActive(newValue == DoorState.Closed);
        }

        [Rpc(SendTo.Server)]
        public void ToggleDoorStateRpc()
        {
            State.Value = State.Value == DoorState.Opened ? DoorState.Closed : DoorState.Opened;
        }
    }
}
