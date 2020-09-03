using System.Collections.Generic;

namespace Squash.Shared.Data.Dto
{
    public class PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<PointsDto> Points { get; set; } = new List<PointsDto>();
        public IEnumerable<TournamentDto> Tournaments { get; set; } = new List<TournamentDto>();
        public StatisticsDto Statistics { get; set; }
    }
}
