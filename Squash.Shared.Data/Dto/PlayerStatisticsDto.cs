using System.Collections.Generic;

namespace Squash.Shared.Data.Dto
{
    public class PlayerStatisticsDto
    {
        public string Name { get; set; }
        public int Wins { get; set; }
        public int Lost { get; set; }
        public int TournamentCount { get; set; }
        public int MatchesCount { get; set; }
        public IEnumerable<PointsDto> PointResults { get; set; }
        public IList<GamesStatDto> OponentStat { get; set; } = new List<GamesStatDto>();
    }
}
