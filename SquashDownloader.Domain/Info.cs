using System;

namespace Squash.Domain
{
    public class Info
    {
        public int Id { get; set; }

        public DateTime LastRun { get; set; }

        public DateTime LastTournamentProcessed { get; set; }
    }
}
