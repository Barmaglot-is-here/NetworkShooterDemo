using Assets.Game.Scripts.Server;
using System.Collections.Generic;
using Unity.Netcode;

namespace Assets.Game.Scripts.Services
{
    public class SynchronizationService : NetworkBehaviour
    {
        public void SyncPlayerList(ulong clientId)
        {
            if (clientId == OwnerClientId)
                return;

            ClientRpcParams rpcParams = new();
            rpcParams.Send.TargetClientIds = new List<ulong> { clientId };

            foreach (var playerId in ServerManager.PlayerList.TeamOne)
                AddPlayerClientRpc(playerId, 0, rpcParams);

            foreach (var playerId in ServerManager.PlayerList.TeamTwo)
                AddPlayerClientRpc(playerId, 1, rpcParams);
        }

        [ClientRpc]
        private void AddPlayerClientRpc(ulong clientId, int team, ClientRpcParams rpcParams) 
            => ServerManager.PlayerList.AddPlayer(clientId, team);
    }
}
