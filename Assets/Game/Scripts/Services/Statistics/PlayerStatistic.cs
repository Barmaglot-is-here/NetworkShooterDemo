using System;
using Unity.Netcode;

namespace Assets.Game.Scripts.Services.StatisticsCount
{
    public struct PlayerStatistic : INetworkSerializable, IEquatable<PlayerStatistic>
    {
        public ulong OwnerId;
        public int KillCount;
        public int DeathCount;
        public int AppliedDamage;
        public int RecivedDamage;

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref KillCount);
            serializer.SerializeValue(ref DeathCount);
            serializer.SerializeValue(ref AppliedDamage);
            serializer.SerializeValue(ref RecivedDamage);
        }

        bool IEquatable<PlayerStatistic>.Equals(PlayerStatistic other)
        {
            return OwnerId == other.OwnerId &&
                   KillCount == other.KillCount && DeathCount == other.DeathCount && 
                   AppliedDamage == other.AppliedDamage && RecivedDamage == other.RecivedDamage;
        }
    }
}
