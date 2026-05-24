using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Shooting
{
    public class CrosshairRay : MonoBehaviour
    {
        private Ray _ray;

        private void Start()
        {
            var camera              = FindFirstObjectByType<Camera>();
            Vector2 screenCenter    = new(Screen.width / 2, Screen.height / 2);

            _ray = camera.ScreenPointToRay(screenCenter);
        }

        private void Update()
        {
            Physics.Raycast(_ray, out var hit);

            //Debug.Log(hit.distance);
            Debug.DrawRay(transform.position, Vector3.right, Color.red, 100000);
        }
    }
}
