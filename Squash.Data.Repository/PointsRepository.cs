using Microsoft.EntityFrameworkCore;
using Squash.Data.Interfaces.Repositories;
using Squash.Domain;
using Squash.Shared.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squash.Data.Repository
{
    public class PointsRepository : Repository<Points>, IPointsRepository
    {
        public PointsRepository(SquashContext squashContext) : base(squashContext)
        {
        }

        public async Task<IEnumerable<PlayerWithPointsDto>> GetAllPlayersPoints(bool onlyActive, DateTime? From, DateTime? To)
        {
            var players = from player in _squashContext.Players
                          where !onlyActive || player.IsActive == onlyActive
                          select new PlayerWithPointsDto
                          {
                              Id = player.Id,
                              Name = player.Name,
                              Points =
                          from point in player.Points
                          where (!From.HasValue || From <= point.Date) && (!To.HasValue || point.Date <= To)
                          select new PointsDto { Date = point.Date, Number = double.Parse(point.Number), PlayerId = player.Id }
                          };
            return await players.ToListAsync();

            //IQueryable < Player > points = _squashContext.Players.Include(player => player.Points);
            //if (onlyActive)
            //{
            //    points = points.Where(a => a.IsActive == true);
            //}

            //return await points.Select(player => new PlayerWithPointsDto { Id = player.Id, Name = player.Name, Points = player.Points.Where(point =>
            //{
            //    if (From.HasValue && To.HasValue)
            //    {
            //        return From <= point.Date && point.Date <= To;
            //    }
            //    if (From.HasValue)
            //    {
            //        return From <= point.Date;
            //    }
            //    if (To.HasValue)
            //    {
            //        return point.Date <= To;
            //    }
            //    return true;
            //})
            //    .Select(points => new PointsDto { Date = points.Date, Number = points.Number, PlayerId = player.Id }) }).ToListAsync();
        }
    }
}
