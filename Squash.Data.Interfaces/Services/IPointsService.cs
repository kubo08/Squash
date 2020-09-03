using Squash.Shared.Data.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squash.Data.Interfaces.Services
{
    public interface IPointsService
    {
        Task<IEnumerable<PlayerWithPointsDto>> GetAllPlayersPoints(bool onlyActive, DateTime? From, DateTime? To);

        Task<IEnumerable<PlayerWithPointsDto>> GetAllPlayersPoints(bool onlyActive, int numberOfResults);

        Task<PlayerWithPointsDto> GetAllPlayersPoints(int playerId, DateTime? From, DateTime? To);

        Task<PlayerWithPointsDto> GetAllPlayersPoints(int playerId, int numberOfResults);
    }
}
