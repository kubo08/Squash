using Squash.Domain;
using Squash.Shared.Data.Dto;
using System.Threading.Tasks;

namespace Squash.Data.Interfaces
{
    public interface IPlayerRepository : IRepository<Player>
    {
        Task<PlayerDto> GetPlayerWithStatistics(int Id);

        Task<HeadToHeadDto> GetHeadToHead(int player1, int player2, bool onlyEleven);

        Task<PlayerStatisticsDto> GetPlayerStatistics(int PlayerId, bool onlyEleven);
    }
}
