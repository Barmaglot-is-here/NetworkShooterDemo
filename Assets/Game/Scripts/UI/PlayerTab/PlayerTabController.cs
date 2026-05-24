using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.UI.PlayerTab
{
    [DefaultExecutionOrder(-1)]
    internal class PlayerTabController : MonoBehaviour
    {
        [SerializeField]
        private PlayerTabRow _prefab;

        [SerializeField]
        private Transform _teamOneColoumn;
        [SerializeField]
        private Transform _teamTwoColoumn;

        private Dictionary<ulong, PlayerTabRow> _views;

        public IReadOnlyDictionary<ulong, PlayerTabRow> Views => _views;

        private void Awake()
        {
            _views = new();
        }

        public void AddPlayer(ulong id, int team, string name)
        {
            var coloumn = team == 0 ? _teamOneColoumn : _teamTwoColoumn;
            var view    = Instantiate(_prefab, coloumn);

            view.Show(name);

            _views.Add(id, view);
        }

        public void RemovePlayer(ulong id)
        {
            var view = _views[id];

            Destroy(view.gameObject);
        }
    }
}
