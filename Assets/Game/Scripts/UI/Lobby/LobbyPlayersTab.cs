using Assets.Game.Scripts.Server;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.UI.Lobby;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class LobbyPlayersTab : MonoBehaviour
    {
        [SerializeField]
        private LobbyPlayerView _prefab;

        [SerializeField]
        private Transform _teamOneColoumn;
        [SerializeField]
        private Transform _teamTwoColoumn;

        private Dictionary<ulong, LobbyPlayerView> _teamOneViews;
        private Dictionary<ulong, LobbyPlayerView> _teamTwoViews;

        private void Awake()
        {
            _teamOneViews = new();
            _teamTwoViews = new();
        }

        private void OnPlayerAdd(ulong playerId, int team)
        {
            var coloumn = team == 0 ? _teamOneColoumn : _teamTwoColoumn;
            var view    = Instantiate(_prefab, coloumn);

            view.Show(playerId);

            var viewsList = team == 0 ? _teamOneViews : _teamTwoViews;

            viewsList.Add(playerId, view);
        }

        private void OnPlayerRemove(ulong playerId, int team)
        {
            var viewsList   = team == 0 ? _teamOneViews : _teamTwoViews;
            var view        = viewsList[playerId];

            Destroy(view.gameObject);
        }

        private void OnEnable()
        {
            ServerManager.PlayerList.OnAdd += OnPlayerAdd;
            ServerManager.PlayerList.OnRemove += OnPlayerRemove;
        }

        private void OnDisable()
        {
            ServerManager.PlayerList.OnAdd -= OnPlayerAdd;
            ServerManager.PlayerList.OnRemove -= OnPlayerRemove;
        }
    }
}
