using Assets.Game.Scripts.Gameplay.Shooting;
using Assets.Game.Scripts.Presentation.Lobby;
using Assets.Game.Scripts.Services.ProfileManagement;
using UnityEngine;

namespace Assets.Game.Scripts.UI.WeaponSelection
{
    public class WeaponSelectionHandler : MonoBehaviour
    {
        [SerializeField]
        private WeaponSelectionContainer _weaponSelectionContainer;
        [SerializeField]
        private LobbyCharacterView _lobbyCharacterView;

        private void Awake()
        {
            _weaponSelectionContainer.OnWeaponSelected += OnWeaponSelected;
        }

        private void OnWeaponSelected(GameObject weapon)
        {
            _lobbyCharacterView.SetupWeapon(weapon.gameObject);

            string playerName = ProfileManager.LocalPlayerProfile.PlayerName;
            ProfileManager.LocalPlayerProfile = new(playerName, weapon.name);
        }
    }
}
