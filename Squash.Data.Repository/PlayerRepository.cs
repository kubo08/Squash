using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Squash.Data.Interfaces;
using Squash.Domain;
using Squash.Shared.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Squash.Data.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {
        public PlayerRepository(SquashContext squashContext) : base(squashContext)
        {
        }

        public async Task<PlayerDto> GetPlayerWithStatistics(int Id)
        {
            var dbPlayer = await _squashContext.Players.Include(player => player.Points).Include(player=>player.TournamentResults).ThenInclude(player=>player.Tournament).ThenInclude(tournament=>tournament.Matches).SingleAsync(a => a.Id == Id);
            var player = new PlayerDto();
            //var player = new PlayerDto { Id = dbPlayer.Id, Name = dbPlayer.Name, Points = dbPlayer.Points.Select(a => new PointsDto { Date = a.Date, Number = a.Number }), Tournaments = dbPlayer.TournamentResults.Select(a=>new TournamentDto{Result= a.Points, Wins=a.Tournament.Matches.Where(a=>a.Player1.Id };

            return player;
        }

        public async Task<HeadToHeadDto> GetHeadToHead(int player1, int player2, bool onlyEleven)
        {
            var matches1 = _squashContext.Matches.Include(a => a.Player1).Include(a => a.Player2).Include(a => a.Games).Include(a => a.Tournament).Where(a => (a.Player1.Id == player1) && (a.Player2.Id == player2) && a.IsJogo);
            var matches2 = _squashContext.Matches.Include(a => a.Player1).Include(a => a.Player2).Include(a => a.Games).Include(a => a.Tournament).Where(a => (a.Player2.Id == player1) && (a.Player1.Id == player2) && a.IsJogo);
            var match = await matches1.AnyAsync() ? await matches1.FirstOrDefaultAsync() : await matches2.FirstOrDefaultAsync();
            if (match == null)
            {
                return new HeadToHeadDto { Player1 = "xx", Player2 = "xyy", MatchesNumber = 0 };
            }
            if (onlyEleven)
            {
                matches1 = matches1.Where(a => a.Tournament.Date >= new DateTime(2020, 6, 1));
                matches2 = matches2.Where(a => a.Tournament.Date >= new DateTime(2020, 6, 1));
            }

            var result = new HeadToHeadDto
            {
                Player1 = match.Player1.Id == player1 ? match.Player1.Name : match.Player2.Name,
                Player2 = match.Player2.Id == player2 ? match.Player2.Name : match.Player1.Name,
                MatchesNumber = await matches1.CountAsync() + await matches2.CountAsync(),
                Wins1 = await matches1.CountAsync(a => a.Games.Any(b => b.Player1Score > b.Player2Score)) + await matches2.CountAsync(a => a.Games.Any(b => b.Player2Score > b.Player1Score)),
                Wins2 = await matches1.CountAsync(a => a.Games.Any(b => b.Player1Score < b.Player2Score)) + await matches2.CountAsync(a => a.Games.Any(b => b.Player2Score < b.Player1Score))
            };

            var games = matches1.Select(a => new GameDto { Score1 = a.Games.First().Player1Score, Score2 = a.Games.First().Player2Score, Date=a.Tournament.Date });
            games = games.Concat(matches2.Select(a => new GameDto { Score1 = a.Games.First().Player2Score, Score2 = a.Games.First().Player1Score, Date = a.Tournament.Date }));

            result.Games = games.OrderByDescending(a=>a.Date);
            return result;
        }

        public async Task<PlayerStatisticsDto> GetPlayerStatistics(int PlayerId, bool onlyEleven) 
        {
            var player = await _squashContext.Players.AsNoTracking().Include(a => a.TournamentResults).ThenInclude(b => b.Tournament).FirstAsync(b => b.Id == PlayerId);
            var result = new PlayerStatisticsDto { Name = player.Name };
            var matches = _squashContext.Matches.AsNoTracking().Include(b => b.Games).Include(b => b.Tournament).Where(a => (a.Player1.Id == PlayerId || a.Player2.Id == PlayerId) && a.IsJogo);
            var points = _squashContext.PlayerTournamentResults.AsNoTracking().Include(b => b.Tournament).Where(a => a.Player.Id == PlayerId);
            if (onlyEleven)
            {
                matches = matches.Where(a => a.Tournament.Date >= new DateTime(2020, 6, 1));
                points = points.Where(a => a.Tournament.Date >= new DateTime(2020, 6, 1));
            }
            result.PointResults = points.Where(a => a.Points.HasValue).Select(b => new PointsDto { Number = b.Points.Value, Date = b.Tournament.Date });
            result.TournamentCount = points.Count();
            result.Lost = await matches.CountAsync(a => (a.Player1.Id == PlayerId ? a.Games.First().Player1Score < a.Games.First().Player2Score : a.Games.First().Player1Score > a.Games.First().Player2Score));
            result.Wins = await matches.CountAsync(a => (a.Player1.Id == PlayerId ? a.Games.First().Player1Score > a.Games.First().Player2Score : a.Games.First().Player1Score < a.Games.First().Player2Score));
            result.MatchesCount = await matches.CountAsync();

            var players = _squashContext.Players.Where(a => a.IsActive && a.Id!= PlayerId);
            foreach(var activePlayer in players.ToList())
            {
                result.OponentStat.Add(await GetGamesStat(PlayerId, activePlayer, onlyEleven));
            }

            return result;
        }

        private async Task<GamesStatDto> GetGamesStat(int Id1, Player player2, bool onlyEleven)
        {
            var matches = _squashContext.Matches.AsNoTracking().Include(b => b.Games).Include(b => b.Tournament).Where(a => ((a.Player1.Id == Id1 && a.Player2.Id==player2.Id) || (a.Player2.Id == Id1 && a.Player1.Id== player2.Id)) && a.IsJogo);
            if (onlyEleven)
            {
                matches = matches.Where(a => a.Tournament.Date > new DateTime(2020, 6, 1));
            }
            var result = new GamesStatDto { Name = player2.Name };
            var count = await matches.CountAsync();
            result.Lost = await matches.CountAsync(a => (a.Player1.Id == Id1 ? a.Games.First().Player1Score < a.Games.First().Player2Score : a.Games.First().Player1Score > a.Games.First().Player2Score));
            result.Wins = await matches.CountAsync(a => (a.Player1.Id == Id1 ? a.Games.First().Player1Score > a.Games.First().Player2Score : a.Games.First().Player1Score < a.Games.First().Player2Score));

            return result;
        }
    }
}
