using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts.Presentation
{
    //Скрипт для отключения отображения персонажа игрока на севере
    public class CharacterServerViewController : NetworkBehaviour
    {
        [SerializeField]
        private SkinnedMeshRenderer _skinnedMeshRenderer;

        public override void OnNetworkSpawn()
        {
            if (IsOwner)
                _skinnedMeshRenderer.renderingLayerMask = 0;
        }
    }
}
