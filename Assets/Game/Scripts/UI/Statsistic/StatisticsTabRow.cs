using Assets.Game.Scripts.Services.StatisticsCount;
using TMPro;
using UnityEngine;

namespace Assets.Game.Scripts.UI.Statsistic
{
    public class StatisticsTabRow : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _playerName;
        [SerializeField]
        private TMP_Text _kills;
        [SerializeField]
        private TMP_Text _deaths;

        public void Show(string playerName, PlayerStatistics statistic)
        {
            _playerName.text    = playerName;
            _kills.text         = statistic.KillCount.ToString();
            _deaths.text        = statistic.DeathCount.ToString();
        }
    }
}
