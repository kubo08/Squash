using Squash.Data.Interfaces.Repositories;
using Squash.Data.Interfaces.Services;
using Squash.Shared.Data.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squash.Web.Shared.Services
{
    public class PointsService : IPointsService
    {
        private readonly IPointsRepository _pointsRepository;

        public PointsService(IPointsRepository pointsRepository)
        {
            _pointsRepository = pointsRepository;
        }

        public Task<IEnumerable<PlayerWithPointsDto>> GetAllPlayersPoints(bool onlyActive, DateTime? From, DateTime? To)
        {
            return _pointsRepository.GetAllPlayersPoints(onlyActive,From,To);
        }
    }
}
