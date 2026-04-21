using Assets.Game.Scripts.Server;
using Assets.Game.Scripts.Server.Spawn;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.TeamManagement;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField]
        private SynchronizationService _synchronizationService;
        [SerializeField]
        private CharacterSpawnService _spawnService;
        [SerializeField]
        private LobbyScreen _lobbyScreen;

        private void Start()
        {
            ServerManager serverManager = new(_synchronizationService, _spawnService, 6);

            _lobbyScreen.Bind(serverManager);
        }
    }
}
