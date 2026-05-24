namespace Assets.Game.Scripts.Services.ServerManagement
{
    //Класс для распределения игроков по командам в соответствии с их показателями (elo, у/c или чем-то таким)
    public static class PlayerDistributor
    {
        public static void Distribute(PlayerList playerList, ulong playerId)
        {
            var teamOne = playerList.TeamOne;
            var teamTwo = playerList.TeamTwo;

            var team = teamOne.Count <= teamTwo.Count ? 0 : 1;

            playerList.AddPlayer(playerId, team);
        }

        //Функция должна распределять игроков заново при выходе одного
        //В демо это не реализовано
        public static void RemoveAndRedistribute(PlayerList playerList, ulong playerId) 
            => playerList.RemovePlayer(playerId);
    }
}
