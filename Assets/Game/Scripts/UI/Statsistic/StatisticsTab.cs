using Assets.Game.Scripts.Services.StatisticsCount;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Statsistic
{
    public class StatisticsTab : MonoBehaviour
    {
        [SerializeField]
        private PlayerStatisticView _prefab;

        [SerializeField]
        private Transform _teamOneColoumn;
        [SerializeField]
        private Transform _teamTwoColoumn;

        private Dictionary<ulong, PlayerStatisticView> _playerStatisticViews;

        private void Awake()
        {
            _playerStatisticViews = new();
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

        private PlayerStatisticView GetView(ulong ownerId)
        {
            PlayerStatisticView view;

            if (!_playerStatisticViews.ContainsKey(ownerId))
            {
                view = Instantiate(_prefab, _teamOneColoumn);

                _playerStatisticViews.Add(ownerId, view);
            }
            else
                view = _playerStatisticViews[ownerId];

            return view;
        }

        private void OnStatAdd(ulong player, PlayerStatistic stat)
        {
            var view = Instantiate(_prefab, _teamOneColoumn);

            view.Show(player, stat);

            _playerStatisticViews.Add(player, view);
        }

        private void OnStatRemove(ulong player)
        {
            var view = Instantiate(_prefab, _teamOneColoumn);

            Destroy(view.gameObject);

            _playerStatisticViews.Remove(player);
        }

        private void OnStatChanged(ulong player, PlayerStatistic statistic)
        {
            var view = _playerStatisticViews[player];

            view.Show(player, statistic);
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
