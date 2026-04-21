using Unity.Netcode;

namespace Assets.Game.Scripts.Services.TeamManagement
{
    //Класс отвечающий за распределение игроков по командам в соответствии с их силой
    //Сейчас ничего такого не делает. Просто формирует равные по количеству команды.
    public class TeamManager : NetworkBehaviour    
    {
        public void DistributePlayer(PlayerList playerList, ulong playerId)
        {
            if (!IsServer)
                return;

            var teamOne = playerList.TeamOne;
            var teamTwo = playerList.TeamTwo;

            var team = teamOne.Count <= teamTwo.Count ? 0 : 1;

            playerList.AddPlayer(playerId, team);
        }
            
        public void RemoveAndDistribute(PlayerList playerList, ulong playerId)
        {
            playerList.RemovePlayer(playerId);
        }
    }
}
