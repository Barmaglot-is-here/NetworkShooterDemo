using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Damage
{
    public class DamageObject : NetworkBehaviour
    {
        [field: SerializeField]
        public int Damage { get; private set; }
    }
}
