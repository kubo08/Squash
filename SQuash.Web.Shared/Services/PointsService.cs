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

        public Task<IEnumerable<PlayerWithPointsDto>> GetAllPlayersPoints(bool onlyActive, int numberOfResults)
        {
            return _pointsRepository.GetAllPlayersPoints(onlyActive, numberOfResults);
        }

        public Task<PlayerWithPointsDto> GetAllPlayersPoints(int playerId, DateTime? From, DateTime? To)
        {
            return _pointsRepository.GetAllPlayersPoints(playerId, From, To);
        }

        public Task<PlayerWithPointsDto> GetAllPlayersPoints(int playerId, int numberOfResults)
        {
            return _pointsRepository.GetAllPlayersPoints(playerId, numberOfResults);
        }
    }
}
