using Unity.Netcode;

namespace Assets.Game.Scripts.Services.Connection
{
    public class ConnectionService
    {
        public readonly int MaxPlayersCount;
        public int PlayersCount => NetworkManager.Singleton.ConnectedClientsIds.Count; 

        public ConnectionService(ServerSettings settings)
        {
            MaxPlayersCount = settings.MaxPlayerCount;
        }

        public bool TryConnect()
        {
            if (PlayersCount >= MaxPlayersCount)
                return false;

            NetworkManager.Singleton.StartClient();

            return true;
        }

        public void Disconnect() => NetworkManager.Singleton.Shutdown();
    }
}
