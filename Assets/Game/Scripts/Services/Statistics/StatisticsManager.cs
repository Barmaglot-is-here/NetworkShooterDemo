using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Services.StatisticsCount
{
    [DisallowMultipleComponent]
    public class StatisticsManager : NetworkBehaviour
    {
        public static StatisticsManager Instance { get; private set; }

        public Dictionary<ulong, PlayerStats> Stats { get; private set; }

        public event Action<ulong, PlayerStats> OnChanged;
        public event Action<ulong, PlayerStats> OnStatAdd;
        public event Action<ulong> OnStatRemove;

        private void Awake()
        {
            Instance    = this;
            Stats       = new();
        }

        public void AddPlayer(ulong id)
        {
            PlayerStats stat = new();

            Stats.Add(id, stat);

            OnStatAdd?.Invoke(id, stat);
        }

        public void RemovePlayer(ulong id)
        {
            Stats.Remove(id);

            OnStatRemove?.Invoke(id);
        }

        public void CountKill(ulong targetId, ulong killerId)
        {
            var targetStats = Stats[targetId];
            var killerStats = Stats[killerId];

            targetStats.DeathCount++;
            killerStats.KillCount++;

            UpdateStatsRpc(targetId, targetStats);
            UpdateStatsRpc(killerId, killerStats);
        }

        public void CountDamage(ulong reciverId, ulong senderId, int damage)
        {
            var reciverStats    = Stats[reciverId];
            var senderStats     = Stats[senderId];

            reciverStats.RecivedDamage += damage;
            senderStats.AppliedDamage += damage;

            UpdateStatsRpc(reciverId, reciverStats);
            UpdateStatsRpc(senderId, senderStats);
        }

        [Rpc(SendTo.Everyone)]
        private void UpdateStatsRpc(ulong playerId, PlayerStats stats)
        {
            Stats[playerId] = stats;

            OnChanged?.Invoke(playerId, stats);
        }
    }
}
