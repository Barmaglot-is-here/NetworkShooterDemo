using System;
using Unity.Netcode;

namespace Assets.Game.Scripts.Services.StatisticsCount
{
    public struct PlayerStats : INetworkSerializable, IEquatable<PlayerStats>
    {
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

        bool IEquatable<PlayerStats>.Equals(PlayerStats other)
        {
            return KillCount == other.KillCount && DeathCount == other.DeathCount && 
                   AppliedDamage == other.AppliedDamage && RecivedDamage == other.RecivedDamage;
        }
    }
}
