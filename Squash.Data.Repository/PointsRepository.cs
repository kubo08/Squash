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

        public async Task<IEnumerable<PlayerWithPointsDto>> GetAllPlayersPoints(bool onlyActive, int numberofResults)
        {
            var players = await (from player in _squashContext.Players
                                 where !onlyActive || player.IsActive == onlyActive
                                 select new PlayerWithPointsDto
                                 {
                                     Id = player.Id,
                                     Name = player.Name,
                                     Points =
                                 (from point in player.Points
                                 orderby point.Date descending
                                 select new PointsDto { Date = point.Date, Number = double.Parse(point.Number), PlayerId = player.Id }).Take(numberofResults)
                                 }).ToListAsync();
            foreach (var player in players)
            {
                var result = await GetLatestResult(player.Id);
                var points = player.Points.FirstOrDefault();
                player.Points = player.Points.Append(new PointsDto { Number = points.Number + result, Date = DateTime.Today, PlayerId = player.Id });
            }

            return players;
        }

        public async Task<IEnumerable<PlayerWithPointsDto>> GetAllPlayersPoints(bool onlyActive, DateTime? From, DateTime? To)
        {
            var players = await (from player in _squashContext.Players
                          where !onlyActive || player.IsActive == onlyActive
                          select new PlayerWithPointsDto
                          {
                              Id = player.Id,
                              Name = player.Name,
                              Points =
                          from point in player.Points
                          where (!From.HasValue || From <= point.Date) && (!To.HasValue || point.Date <= To)
                          select new PointsDto { Date = point.Date, Number = double.Parse(point.Number), PlayerId = player.Id }
                          }).ToListAsync();
            foreach(var player in players)
            {
                var result =await GetLatestResult(player.Id);
                var points = player.Points.LastOrDefault();
                player.Points = player.Points.Append(new PointsDto { Number = points.Number + result, Date = DateTime.Today, PlayerId = player.Id });
            }

            return players;

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

        public async Task<PlayerWithPointsDto> GetAllPlayersPoints(int playerId, DateTime? From, DateTime? To)
        {
            var sqPlayer = await (from player in _squashContext.Players
                                  where player.Id == playerId
                                  select new PlayerWithPointsDto
                                  {
                                      Id = player.Id,
                                      Name = player.Name,
                                      Points =
                                  from point in player.Points
                                  where (!From.HasValue || From <= point.Date) && (!To.HasValue || point.Date <= To)
                                  select new PointsDto { Date = point.Date, Number = double.Parse(point.Number), PlayerId = player.Id }
                                  }).FirstAsync();
            var result = await GetLatestResult(playerId);
            var points = sqPlayer.Points.LastOrDefault();
            sqPlayer.Points = sqPlayer.Points.Append(new PointsDto { Number = points.Number + result, Date = DateTime.Today, PlayerId = playerId });

            return sqPlayer;
        }

        public async Task<PlayerWithPointsDto> GetAllPlayersPoints(int playerId, int numberOfResult)
        {
            var sqPlayer = await (from player in _squashContext.Players
                                  where player.Id == playerId
                                  select new PlayerWithPointsDto
                                  {
                                      Id = player.Id,
                                      Name = player.Name,
                                      Points =
                                  (from point in player.Points
                                  orderby point.Date descending
                                  select new PointsDto { Date = point.Date, Number = double.Parse(point.Number), PlayerId = player.Id }).Take(numberOfResult)
                                  }).FirstAsync();
            var result = await GetLatestResult(playerId);
            var points = sqPlayer.Points.FirstOrDefault();
            sqPlayer.Points = sqPlayer.Points.Append(new PointsDto { Number = points.Number + result, Date = DateTime.Today, PlayerId = playerId }).OrderBy(a=>a.Date);

            return sqPlayer;
        }

        public async Task<double> GetLatestResult(int playerId)
        {
            var lastTournament = (await _squashContext.Players.Include(a => a.PlayerTournaments).ThenInclude(b => b.Tournament).FirstAsync(a => a.Id == playerId))
                    .PlayerTournaments.OrderByDescending(a => a.Tournament.Date).FirstOrDefault();
            var result = (await _squashContext.PlayerTournamentResults.FirstAsync(a => a.Player.Id == playerId && a.Tournament.Id == lastTournament.TournamentId)).Points;

            return result ?? 0;
        }
    }
}
