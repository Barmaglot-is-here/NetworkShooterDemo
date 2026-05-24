using Assets.Game.Scripts.Utils;
using Assets.Game.Scripts.Utils.Synchronization;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Services
{
    public class PlayerList/* : NetworkVariableBase*/
    {
        [Serializable]
        private class Delta
        {
            public DeltaType DeltaType;
            public ulong Value;
            public int Team;

            public Delta(DeltaType deltaType, ulong value, int team)
            {
                DeltaType = deltaType;
                Value = value;
                Team = team;
            }
        }

        private Delta _delta;

        private readonly List<ulong> _teamOne;
        private readonly List<ulong> _teamTwo;

        public IReadOnlyList<ulong> TeamOne => _teamOne;
        public IReadOnlyList<ulong> TeamTwo => _teamTwo;

        public delegate void PlayerListChangedDelegate(ulong playerId, int team);

        public event PlayerListChangedDelegate OnAdd;
        public event PlayerListChangedDelegate OnRemove;

        public PlayerList()
        {
            _teamOne = new();
            _teamTwo = new();
        }

        public void AddPlayer(ulong id, int team)
        {
            AddPlayerNoSync(id, team);

            _delta = new(DeltaType.Add, id, team);

            //SetDirty(true);
        }

        private void AddPlayerNoSync(ulong id, int team)
        {
            var teamList = team == 0 ? _teamOne : _teamTwo;

            teamList.Add(id);

            OnAdd?.Invoke(id, team);
        }

        public void RemovePlayer(ulong id)
        {
            int team = GetTeam(id);

            RemovePlayerNoSync(id, team);

            _delta = new(DeltaType.Remove, id, team);

            //SetDirty(true);
        }

        private void RemovePlayerNoSync(ulong id, int team)
        {
            var teamList = GetTeamList(id);

            teamList.Remove(id);

            OnRemove?.Invoke(id, team);
        }

        private IList<ulong> GetTeamList(ulong playerId)
            => _teamOne.Contains(playerId) ? _teamOne : _teamTwo;

        public int GetTeam(ulong playerId)
        {
            if (_teamOne.Contains(playerId))
                return 0;

            if (_teamTwo.Contains(playerId))
                return 1;

            throw new Exception($"Unknown player: {playerId}");
        }

        //public override void WriteDelta(FastBufferWriter writer)
        //{
        //    var delta = JsonUtility.ToJson(_delta);

        //    writer.WriteValueSafe(delta);
        //}

        //public override void WriteField(FastBufferWriter writer)
        //{
        //    var teamOne = JsonUtility.ToJson(new SerializableList<ulong>(_teamOne));
        //    var teamTwo = JsonUtility.ToJson(new SerializableList<ulong>(_teamTwo));

        //    writer.WriteValueSafe(teamOne);
        //    writer.WriteValueSafe(teamTwo);
        //}

        //public override void ReadField(FastBufferReader reader)
        //{
        //    reader.ReadValueSafe(out string teamOne);
        //    reader.ReadValueSafe(out string teamTwo);

        //    var listOne = JsonUtility.FromJson<SerializableList<ulong>>(teamOne);
        //    var listTwo = JsonUtility.FromJson<SerializableList<ulong>>(teamTwo);

        //    foreach (var player in listOne.Value)
        //        AddPlayerNoSync(player, 0);
        //    foreach (var player in listTwo.Value)
        //        AddPlayerNoSync(player, 1);
        //}

        //public override void ReadDelta(FastBufferReader reader, bool keepDirtyDelta)
        //{
        //    reader.ReadValueSafe(out string json);

        //    var delta = JsonUtility.FromJson<Delta>(json);

        //    if (delta.DeltaType == DeltaType.Add)
        //        AddPlayerNoSync(delta.Value, delta.Team);
        //    else
        //        RemovePlayerNoSync(delta.Value, delta.Team);
        //}
    }
}
