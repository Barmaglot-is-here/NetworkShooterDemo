using Assets.Game.Scripts.Gameplay.Interaction;
using UnityEngine;

namespace Assets.Game.Scripts.Level
{
    public class ToggleDoorButton : InteractionTarget
    {
        [SerializeField]
        private TeamBaseDoor _door;

        public override void Interact() => _door.ToggleDoorStateRpc();
    }
}
