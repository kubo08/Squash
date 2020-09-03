using System;
using System.Collections.Generic;
using System.Text;

namespace Squash.Domain
{
    public class PlayerTournament
    {
        public int PlayerId { get; set; }
        public int TournamentId { get; set; }
        public Player Player { get; set; }
        public Tournament Tournament { get; set; }
    }
}
