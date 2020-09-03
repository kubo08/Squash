namespace Squash.Domain
{
    public class PlayerTournamentResult
    {
        public int Id { get; set; }

        public double? Points { get; set; }

        public int Position { get; set; }

        public Player Player { get; set; }

        public Tournament Tournament { get; set; }
    }
}
