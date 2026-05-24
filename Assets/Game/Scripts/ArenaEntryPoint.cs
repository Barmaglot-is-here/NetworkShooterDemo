using Assets.Game.Scripts.Server.Spawn;
using Assets.Game.Scripts.Services;
using Assets.Game.Scripts.Services.MatchManagement;
using Assets.Game.Scripts.Services.ProfileManagement;
using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public class ArenaEntryPoint : NetworkBehaviour
    {
        [SerializeField]
        private CharacterSpawnService _characterSpawnService;

        private MatchManager _matchManager;

        private void Awake()
        {
            if (!NetworkManager.IsServer)
                return;

            var playerList      = ApplicationServices.Get<PlayerList>();
            var profileManager  = ApplicationServices.Get<ProfileManager>();

            _matchManager = new(playerList, _characterSpawnService, profileManager);

            _characterSpawnService.ReadySignal.Subscribe(_matchManager.Run);
        }
    }
}
