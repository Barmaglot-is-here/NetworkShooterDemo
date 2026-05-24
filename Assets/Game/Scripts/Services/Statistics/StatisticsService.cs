using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Services.StatisticsCount
{
    [DisallowMultipleComponent]
    public class StatisticsService : NetworkBehaviour
    {
        public static StatisticsService Instance { get; private set; }

        public Dictionary<ulong, PlayerStatistics> Stats { get; private set; }

        public event Action<ulong, PlayerStatistics> OnAdd;
        public event Action<ulong, PlayerStatistics> OnChanged;
        public event Action<ulong> OnRemove;

        private void Awake()
        {
            if (Instance != null)
                throw new Exception($"{nameof(StatisticsService)} is Singleton");

            Instance    = this;
            Stats       = new();
        }

        public void AddPlayer(ulong id)
        {
            PlayerStatistics stat = new();

            Stats.Add(id, stat);

            OnAdd?.Invoke(id, stat);
        }

        public void RemovePlayer(ulong id)
        {
            Stats.Remove(id);

            OnRemove?.Invoke(id);
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
        private void UpdateStatsRpc(ulong playerId, PlayerStatistics stats)
        {
            Stats[playerId] = stats;

            OnChanged?.Invoke(playerId, stats);
        }
    }
}
