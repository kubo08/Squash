using Squash.Shared.Data.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squash.Data.Interfaces.Services
{
    public interface IPointsService
    {
        Task<IEnumerable<PlayerWithPointsDto>> GetAllPlayersPoints(bool onlyActive, DateTime? From, DateTime? To);
    }
}
