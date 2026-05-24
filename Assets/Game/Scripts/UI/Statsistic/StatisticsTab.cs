using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.StatisticsCount;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Statsistic
{
    public class StatisticsTab : MonoBehaviour
    {
        [SerializeField]
        private StatisticsTabRow _prefab;

        [SerializeField]
        private Transform _teamOneColoumn;
        [SerializeField]
        private Transform _teamTwoColoumn;

        private Dictionary<ulong, StatisticsTabRow> _rows;

        private PlayerList _playerList;

        public void Bind(PlayerList playerList)
        {
            _playerList = playerList;
        }

        private void Awake()
        {
            _rows = new();
        }

        private void OnEnable()
        {
            FillView(StatisticsService.Instance);

            StatisticsService.Instance.OnAdd += OnStatAdd;
            StatisticsService.Instance.OnRemove += OnStatRemove;
            StatisticsService.Instance.OnChanged += OnStatChanged;
        }

        private void FillView(StatisticsService statisticsManager)
        {
            foreach (var stat in statisticsManager.Stats)
            {
                var view = GetView(stat.Key);

                //view.Show(stat.Key, stat.Value);
            }
        }

        private StatisticsTabRow GetView(ulong playerId)
        {
            StatisticsTabRow view;

            if (!_rows.ContainsKey(playerId))
            {
                int team    = _playerList.GetTeam(playerId);
                view        = CreateView(playerId, team);
            }
            else
                view = _rows[playerId];

            return view;
        }

        private StatisticsTabRow CreateView(ulong playerId, int team)
        {
            var coloumn = team == 0 ? _teamOneColoumn : _teamTwoColoumn;
            var view    = Instantiate(_prefab, coloumn);

            _rows.Add(playerId, view);

            return view;
        }

        private void OnStatAdd(ulong playerId, PlayerStatistics stat)
        {
            int team = _playerList.GetTeam(playerId);
            var view = CreateView(playerId, team);

            //view.Show(playerId, stat);
        }

        private void OnStatRemove(ulong playerId)
        {
            var view = _rows[playerId];

            Destroy(view.gameObject);

            _rows.Remove(playerId);
        }

        private void OnStatChanged(ulong playerId, PlayerStatistics statistic)
        {
            var view = _rows[playerId];

            //view.Show(playerId, statistic);
        }

        private void OnDisable()
        {
            StatisticsService.Instance.OnAdd -= OnStatAdd;
            StatisticsService.Instance.OnRemove -= OnStatRemove;
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
