namespace Assets.Game.Scripts.Services.TeamManagement
{
    //Класс отвечающий за распределение игроков по командам в соответствии с их силой
    //Сейчас ничего такого не делает. Просто формирует равные по количеству команды.
    public static class TeamDistributor    
    {
        public static void Distribute(PlayerList playerList, ulong playerId)
        {
            var teamOne = playerList.TeamOne;
            var teamTwo = playerList.TeamTwo;

            var team = teamOne.Count <= teamTwo.Count ? 0 : 1;

            playerList.AddPlayer(playerId, team);
        }

        public static void RemoveAndDistribute(PlayerList playerList, ulong playerId)
        {
            playerList.RemovePlayer(playerId);
        }
    }
}
