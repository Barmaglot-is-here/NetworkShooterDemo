using UnityEngine;

[CreateAssetMenu(fileName = "ServerSettings", menuName = "Configs/ServerSettings")]
public class ServerSettings : ScriptableObject
{
    [field: SerializeField]
    public int MaxPlayerCount { get; private set; }
}
