using System.Collections.Generic;

namespace Squash.Domain
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public IList<PlayerTournament> PlayerTournaments { get; set; } = new List<PlayerTournament>();

        public IList<Points> Points { get; set; } = new List<Points>();

        public IList<PlayerTournamentResult> TournamentResults { get; set; } = new List<PlayerTournamentResult>();

        public Player(string name)
        {
            Name = name;
        }

        public Player()
        {

        }
        
    }
}
