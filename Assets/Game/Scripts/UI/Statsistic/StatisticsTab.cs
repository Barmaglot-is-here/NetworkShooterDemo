using Assets.Game.Scripts.Server;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.StatisticsCount;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Statsistic
{
    public class StatisticsTab : MonoBehaviour
    {
        [SerializeField]
        private StatsView _prefab;

        [SerializeField]
        private Transform _teamOneColoumn;
        [SerializeField]
        private Transform _teamTwoColoumn;

        private Dictionary<ulong, StatsView> _statsViews;

        private void Awake()
        {
            _statsViews = new();
        }

        private void OnEnable()
        {
            FillView(StatisticsManager.Instance);

            StatisticsManager.Instance.OnStatAdd += OnStatAdd;
            StatisticsManager.Instance.OnStatRemove += OnStatRemove;
            StatisticsManager.Instance.OnChanged += OnStatChanged;
        }

        private void FillView(StatisticsManager statisticsManager)
        {
            foreach (var stat in statisticsManager.Stats)
            {
                var view = GetView(stat.Key);

                view.Show(stat.Key, stat.Value);
            }
        }

        private StatsView GetView(ulong playerId)
        {
            StatsView view;

            if (!_statsViews.ContainsKey(playerId))
            {
                int team    = ServerManager.PlayerList.GetTeam(playerId);
                view        = CreateView(playerId, team);
            }
            else
                view = _statsViews[playerId];

            return view;
        }

        private StatsView CreateView(ulong playerId, int team)
        {
            var coloumn = team == 0 ? _teamOneColoumn : _teamTwoColoumn;
            var view    = Instantiate(_prefab, coloumn);

            _statsViews.Add(playerId, view);

            return view;
        }

        private void OnStatAdd(ulong playerId, PlayerStats stat)
        {
            int team = ServerManager.PlayerList.GetTeam(playerId);
            var view = CreateView(playerId, team);

            view.Show(playerId, stat);
        }

        private void OnStatRemove(ulong playerId)
        {
            var view = _statsViews[playerId];

            Destroy(view.gameObject);

            _statsViews.Remove(playerId);
        }

        private void OnStatChanged(ulong playerId, PlayerStats statistic)
        {
            var view = _statsViews[playerId];

            view.Show(playerId, statistic);
        }

        private void OnDisable()
        {
            StatisticsManager.Instance.OnStatAdd -= OnStatAdd;
            StatisticsManager.Instance.OnStatRemove -= OnStatRemove;
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
