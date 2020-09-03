using Microsoft.AspNetCore.Mvc;
using Squash.Data.Interfaces.Services;
using Squash.Shared.Data.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Squash.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PointsController : ControllerBase
    {
        private IPointsService pointsService;
        public PointsController(IPointsService pointsService)
        {
            this.pointsService = pointsService;
        }

        [HttpGet]
        public async Task<IEnumerable<PlayerWithPointsDto>> Get([FromQuery] bool onlyActive = true, [FromQuery] DateTime? From = null, [FromQuery] DateTime? To = null)
        {
            return await pointsService.GetAllPlayersPoints(onlyActive,From,To);
        }
    }
}
