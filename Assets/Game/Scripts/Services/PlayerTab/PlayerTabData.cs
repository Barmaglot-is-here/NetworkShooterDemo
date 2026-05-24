using System;

namespace Assets.Game.Scripts.Services.PlayerTab
{
    [Serializable]
    public struct PlayerTabData
    {
        public ulong Id;
        public int Team;
        public string Name;

        public PlayerTabData(ulong id, int team, string name)
        {
            Id = id;
            Team = team;
            Name = name;
        }
    }
}
