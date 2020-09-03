using System;
using System.Collections.Generic;
using System.Text;

namespace Squash.Shared.Data.Dto
{
    public class PlayerWithPointsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<PointsDto> Points { get; set; } = new List<PointsDto>();
    }
}
