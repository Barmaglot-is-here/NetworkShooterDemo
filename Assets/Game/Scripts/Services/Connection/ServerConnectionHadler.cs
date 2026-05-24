using Assets.Game.Scripts.Services.ProfileManagement;
using Assets.Game.Scripts.Services.ServerManagement;
using System;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Services.Connection
{
    public class ServerConnectionHadler : IDisposable
    {
        private readonly PlayerList _playerList;
        private readonly ProfileManager _profileManager;

        public ServerConnectionHadler(PlayerList playerList, ProfileManager profileManager)
        {
            _playerList     = playerList;
            _profileManager = profileManager;

            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;

            NetworkManager.Singleton.CustomMessagingManager
                .RegisterNamedMessageHandler(ClientConnectionHandler.SEND_PROFILE_MESSAGE,
                                             RecivePlayerProfile);
        }

        private void RecivePlayerProfile(ulong senderClientId, FastBufferReader reader)
        {
            reader.ReadValueSafe(out string json);

            var profile = JsonUtility.FromJson<PlayerProfile>(json);

            _profileManager.Add(senderClientId, profile);

            Debug.Log("Refactoring");

            PlayerDistributor.Distribute(_playerList, senderClientId);
        }

        public void Dispose()
        {
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnectCallback;
        }

        private void OnClientDisconnectCallback(ulong id)
        {
            PlayerDistributor.RemoveAndRedistribute(_playerList, id);

            _profileManager.Remove(id);
        }
    }
}
