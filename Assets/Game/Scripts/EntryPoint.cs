using Assets.Game.Scripts.Server;
using Assets.Game.Scripts.Server.Spawn;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private CharacterSpawnService _spawnService;
        [SerializeField]
        private LobbyScreen _lobbyScreen;

        private void Start()
        {
            ServerManager connectionService = new(_spawnService, 6);

            _lobbyScreen.Bind(connectionService);
        }
    }
}
