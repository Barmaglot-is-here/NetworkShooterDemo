using System.Collections.Generic;

namespace Assets.Game.Scripts.Services
{
    public class PlayerList
    {
        private readonly List<ulong> _teamOne;
        private readonly List<ulong> _teamTwo;

        public IReadOnlyList<ulong> TeamOne => _teamOne;
        public IReadOnlyList<ulong> TeamTwo => _teamTwo;

        public delegate void TeamListChangedDelegate(ulong playerId, int team);

        public event TeamListChangedDelegate OnAdd;
        public event TeamListChangedDelegate OnRemove;

        public PlayerList()
        {
            _teamOne = new();
            _teamTwo = new();
        }

        public void AddPlayer(ulong id, int team)
        {
            var teamList = team == 0 ? _teamOne : _teamTwo;

            teamList.Add(id);

            OnAdd.Invoke(id, team);
        }

        public void RemovePlayer(ulong id)
        {
            var teamList    = GetTeamList(id);
            int teamIndex   = GetTeam(id);

            teamList.Remove(id);

            OnRemove?.Invoke(id, teamIndex);
        }

        private IList<ulong> GetTeamList(ulong playerId)
            => _teamOne.Contains(playerId) ? _teamOne : _teamTwo;

        public int GetTeam(ulong playerId)
            => _teamOne.Contains(playerId) ? 0 : 1;
    }
}
