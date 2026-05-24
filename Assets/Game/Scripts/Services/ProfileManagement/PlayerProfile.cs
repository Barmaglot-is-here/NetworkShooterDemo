namespace Assets.Game.Scripts.Services.ProfileManagement
{
    public struct PlayerProfile
    {
        public string PlayerName;
        public string WeaponName;

        public PlayerProfile(string playerName, string weaponName)
        {
            PlayerName = playerName;
            WeaponName = weaponName;
        }
    }
}
