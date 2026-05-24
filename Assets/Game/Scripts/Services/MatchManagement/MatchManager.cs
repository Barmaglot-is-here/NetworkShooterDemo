using Assets.Game.Scripts.Server.Spawn;
using Assets.Game.Scripts.Services.ProfileManagement;
using Assets.Game.Scripts.Services.Spawn;
using Assets.Game.Scripts.Services.StatisticsCount;
using System;
using System.Linq;

namespace Assets.Game.Scripts.Services.MatchManagement
{
    public class MatchManager
    {
        private readonly PlayerList _playerList;
        private readonly CharacterSpawnService _spawnService;
        private readonly ProfileManager _profileManager;

        public MatchManager(PlayerList playerList, CharacterSpawnService spawnService, 
                            ProfileManager profileManager)
        {
            _playerList = playerList;
            _spawnService = spawnService;
            _profileManager = profileManager;
        }

        public void Run()
        {
            var teamOneProfiles = _playerList.TeamOne
                .Select(id => new SpawnProfile(id, 0, _profileManager.Get(id).WeaponName));
            var teamTwoProfiles = _playerList.TeamTwo
                .Select(id => new SpawnProfile(id, 1, _profileManager.Get(id).WeaponName));

            _spawnService.Spawn(teamOneProfiles);
            _spawnService.Spawn(teamTwoProfiles);

            foreach (var player in _playerList.TeamOne.Concat(_playerList.TeamTwo))
                StatisticsService.Instance.AddPlayer(player);
        }

        public void Complete() => throw new NotImplementedException();
    }
}
