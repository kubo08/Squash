using System;
using System.Collections.Generic;
using System.Text;

namespace Squash.Domain
{
    public class Match
    {
        public int Id { get; set; }

        public Tournament Tournament { get; set; }

        public IList<Game> Games { get; set; } = new List<Game>();

        public bool IsTraining { get; set; }

        public bool IsJogo { get; set; }

        public Player Player1 { get; set; }

        public Player Player2 { get; set; }
    }
}
