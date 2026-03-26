using Assets.Game.Scripts.Infrastructure.Spawn;
using Assets.Game.Scripts.Services;
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
            LobbyService lobby = new(_spawnService);

            _lobbyScreen.Bind(lobby);
        }
    }
}
