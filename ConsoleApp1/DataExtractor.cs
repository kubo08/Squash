using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore.Storage;
using Squash.Data;
using Squash.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBPlayer = Squash.Domain.Player;

namespace ConsoleApp1
{
    public class DataExtractor
    {
        private DateTime date;
        private Tournament tournament;
        private IList<Player> players = new List<Player>();
        private HtmlDocument document = new HtmlDocument();

        public DataExtractor(string data, DateTime date)
        {
            this.date = date;
            tournament = new Tournament(date);
            document.LoadHtml(data);
        }

        public async Task Process()
        {
            GetPlayers();
            using (var _context = new SquashContext())
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    ProcessPlayers(_context);

                    GetResults(_context);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
            } 
        }

        private void GetResults(SquashContext context)
        {
            var rows = document.DocumentNode.SelectNodes("//tbody//tr");
            var position = 1;
            foreach(var row in rows)
            {
                var result = row.SelectNodes("descendant::td");
                ProcessResult(context, tournament, result, position++);
            }
        }

        private void ProcessResult(SquashContext context, Tournament tournament, HtmlNodeCollection results, int position)
        {
            var player = GetPlayer(context, players.ElementAt(position-1).Name);
            for(int i = 2 + position; i < players.Count + 2; i++)
            {
                var player2 = GetPlayer(context, players.ElementAt(i - 2).Name);
                var result = results.ElementAt(i).InnerHtml.Trim().Split(" ");
                AddResults(context, player, player2, result, tournament);
            }
            double rating;
            if (double.TryParse(results.Last().InnerText.Trim().Replace("&nbsp;", ""), out rating))
            {
                player.TournamentResults.Add(new PlayerTournamentResult { Position = position, Points = rating, Tournament = tournament });
            }
            else
            {
                player.TournamentResults.Add(new PlayerTournamentResult { Position = position, Points = null, Tournament = tournament });
            }

            context.SaveChanges();
        }

        private void AddResults(SquashContext context, DBPlayer player1, DBPlayer player2, string[] results, Tournament tournament)
        {
            foreach (var result in results) {
                var score = result.Split(":");
                int homeScore, awayScore;
                if (int.TryParse(score[0], out homeScore) && int.TryParse(score[1], out awayScore))
                {
                    var match = new Match { IsTraining = false, IsJogo=true, Player1 = player1, Player2 = player2, Games = { new Game { Player1Score = homeScore, Player2Score = awayScore } } };
                    tournament.Matches.Add(match);
                    //context.Games.Add(new Game { Player1 = player1, Player2 = player2, Player1Score = homeScore, Player2Score = awayScore, Tournament = tournament });
                }
            }
            context.SaveChanges();
        }

        private void GetPlayers()
        {
            var elements = document.DocumentNode.SelectNodes("//thead//th");
            for(int i = 3; i < elements.Count-2; i++)
            {
                var pl = elements[i].InnerHtml.Split("<br>");
                players.Add(new Player(pl[0], pl[1].Replace("[","").Replace("]","")));
            }
        }

        private void ProcessPlayers(SquashContext context)
        {
            foreach (var player in players)
            {
                var dbPlayer = context.Players.Where(a => a.Name == player.Name.Trim()).FirstOrDefault();
                dbPlayer = CreatePlayer(context, player.Name, dbPlayer);
                AddPoints(context, dbPlayer, date, player.Points);
                tournament.PlayerTournaments.Add(new PlayerTournament { Tournament = tournament, Player = dbPlayer });
            }
            context.SaveChanges();
        }

        private DBPlayer CreatePlayer(SquashContext context, string player, DBPlayer dbPlayer)
        {
            if (dbPlayer != null)
            {
                return dbPlayer;
            }
            dbPlayer = new DBPlayer(player);
            context.Players.Add(dbPlayer);
            context.SaveChanges();
            return dbPlayer;
        }

        private void AddPoints(SquashContext context, DBPlayer player, DateTime date, string points)
        {
            var dbPoints = new Points { Date = date, Number = points };
            player.Points.Add(dbPoints);
        }

        private DBPlayer GetPlayer(SquashContext context, string name)
        {
            return context.Players.Where(a => a.Name == name.Trim()).FirstOrDefault();
        }
    }
}
