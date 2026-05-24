using System;
using Unity.Netcode;

namespace Assets.Game.Scripts.Services.Spawn
{
    [Serializable]
    public struct SpawnProfile : INetworkSerializable
    {
        public ulong PlayerId;
        public int Team;
        public string WeaponName;

        public SpawnProfile(ulong playerId, int team, string weaponName)
        {
            PlayerId = playerId;
            Team = team;
            WeaponName = weaponName;
        }

        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref PlayerId);
            serializer.SerializeValue(ref Team);
            serializer.SerializeValue(ref WeaponName);
        }
    }
}
