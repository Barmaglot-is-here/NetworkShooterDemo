using UnityEngine;

namespace Assets.Game.Scripts.Presentation.Lobby
{
    public class LobbyCharacterView : MonoBehaviour
    {
        [SerializeField]
        private Transform _weaponContainer;

        private GameObject _currentWeapon;

        public void SetupWeapon(GameObject prefab)
        {
            if (_currentWeapon != null)
                Destroy(_currentWeapon);

            _currentWeapon = Instantiate(prefab, _weaponContainer);
        }
    }
}
