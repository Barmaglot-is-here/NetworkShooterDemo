namespace Assets.Game.Scripts.Services.ProfileManagement
{
    public static class ProfileManager
    {
        public static string GetName(ulong playerId) => $"Player_{playerId}";
    }
}
