using UnityEngine;

namespace Assets.Game.Scripts.Server.Spawn
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _rotation;

        public Vector3 Position => transform.position;
        public Quaternion Rotation => Quaternion.Euler(_rotation);
    }
}
