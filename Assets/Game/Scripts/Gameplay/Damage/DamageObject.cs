using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Damage
{
    public class DamageObject : MonoBehaviour
    {
        [field: SerializeField]
        public int Damage { get; private set; }
    }
}
