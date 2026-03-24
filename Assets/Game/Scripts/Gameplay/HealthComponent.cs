using Assets.Rx;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay
{
    public class HealthComponent : MonoBehaviour
    {
        public Rx<int> Value { get; private set; }

        private void Awake()
        {
            Value = new(100);
        }
    }
}
