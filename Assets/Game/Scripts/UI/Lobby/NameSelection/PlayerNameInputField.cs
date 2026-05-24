using Assets.Game.Scripts.Gameplay.Shooting;
using Assets.Game.Scripts.Services.ProfileManagement;
using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Lobby.NameSelection
{
    [RequireComponent(typeof(TMP_InputField))]
    public class PlayerNameInputField : MonoBehaviour
    {
        private TMP_InputField _inputField;

        private void Awake()
        {
            _inputField = GetComponent<TMP_InputField>();

            OnNameChanged(_inputField.text);
        }

        private void OnEnable()
        {
            _inputField.onValueChanged.AddListener(OnNameChanged);
        }

        private void OnDisable()
        {
            _inputField.onValueChanged.RemoveListener(OnNameChanged);
        }

        private void OnNameChanged(string name)
        {
            string weaponName = ProfileManager.LocalPlayerProfile.PlayerName;
            ProfileManager.LocalPlayerProfile = new(name, weaponName);
        }
    }
}
