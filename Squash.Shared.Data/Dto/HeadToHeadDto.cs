using System.Collections.Generic;

namespace Squash.Shared.Data.Dto
{
    public class HeadToHeadDto
    {
        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public int Wins1 { get; set; }

        public int Wins2 { get; set; }

        public int MatchesNumber { get; set; }

        public IEnumerable<GameDto> Games { get; set; }
    }
}
