using Assets.Game.Scripts.Services.PlayerTab;
using Assets.Game.Scripts.UI.PlayerTab;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Lobby.PlayerTab
{
    public class LobbyPlayersTab : MonoBehaviour
    {
        [SerializeField]
        private PlayerTabController _playerTabController;

        [SerializeField]
        private GameObject _loadingIndicator;

        private PlayerTabService _service;

        public void Bind(PlayerTabService service)
        {
            _service = service;
        }

        private void FillView(IEnumerable<PlayerTabData> dataList)
        {
            foreach (var data in dataList)
                OnPlayerAdd(data);
            
            _loadingIndicator.SetActive(false);
        }

        private void OnEnable()
        {
            _service.OnAdd += OnPlayerAdd;
            _service.OnRemove += OnPlayerRemove;

            _service.ReadySignal.Subscribe(() => FillView(_service.DataList));
        }

        private void OnDisable()
        {
            _service.OnAdd -= OnPlayerAdd;
            _service.OnRemove -= OnPlayerRemove;
        }

        private void OnPlayerAdd(PlayerTabData data) 
            => _playerTabController.AddPlayer(data.Id, data.Team, data.Name);

        private void OnPlayerRemove(PlayerTabData data) 
            => _playerTabController.RemovePlayer(data.Id);
    }
}
