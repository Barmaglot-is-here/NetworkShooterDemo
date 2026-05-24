using Assets.Game.Scripts.Services.ProfileManagement;
using System;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Services.Connection
{
    public class ClientConnectionHandler : IDisposable
    {
        public const string SEND_PROFILE_MESSAGE = nameof(SEND_PROFILE_MESSAGE);

        public ClientConnectionHandler()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
        }

        private void OnClientConnectedCallback(ulong id)
        {
            if (id != NetworkManager.Singleton.LocalClientId)
                return;

            SendProfile(ProfileManager.LocalPlayerProfile);
        }

        private void SendProfile(PlayerProfile profile)
        {
            FastBufferWriter writer = new(256, Unity.Collections.Allocator.Temp);
            var json = JsonUtility.ToJson(profile);

            writer.WriteValueSafe(json);

            NetworkManager.Singleton.CustomMessagingManager
                .SendNamedMessage(SEND_PROFILE_MESSAGE, NetworkManager.ServerClientId, writer);
        }

        public void Dispose()
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnectedCallback;
        }
    }
}
