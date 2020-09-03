using System;
using System.Collections.Generic;
using System.Text;

namespace Squash.Domain
{
    public class Tournament
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public IList<Match> Matches { get; set; } = new List<Match>();

        public IList<PlayerTournament> PlayerTournaments { get; set; } = new List<PlayerTournament>();

        public Tournament(DateTime date)
        {
            Date = date;
        }
    }
}
