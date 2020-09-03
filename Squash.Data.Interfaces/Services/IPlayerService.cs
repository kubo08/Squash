using Squash.Domain;
using Squash.Shared.Data.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squash.Data.Interfaces
{
    public interface IPlayerService
    {
        List<Player> GetAllPlayers(bool onlyActive);

        Player GetPlayerById(int Id);

        Task<HeadToHeadDto> GetHeadToHead(int player1Id, int player2Id, bool onlyEleven);
        
        Task<PlayerStatisticsDto> GetPlayerStatistics(int playerId, bool onlyEleven);
    }
}
