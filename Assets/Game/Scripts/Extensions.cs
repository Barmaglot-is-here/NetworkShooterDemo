using Unity.Netcode;
using UnityEngine;

namespace Assets.Game.Scripts
{
    public static class Extensions
    {
        //Fix недоходящих сообщений:
        //Сначала выключаем все компоненты на объекте чтобы избежать отправки сообщений,
        //После вызываем Despawn
        public static void SafeDestroy(this NetworkBehaviour networkBehaviour)
        {
            networkBehaviour.gameObject.DisableAllComponents();

            networkBehaviour.NetworkObject.Despawn();
        }

        public static void DisableAllComponents(this GameObject gameObject)
        {
            foreach (var component in gameObject.GetComponents<MonoBehaviour>())
                component.enabled = false;
        }
    }
}
