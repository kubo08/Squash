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
        public async Task<IEnumerable<PlayerWithPointsDto>> Get([FromQuery] bool onlyActive = true, [FromQuery] int numberOfResults = 0, [FromQuery] DateTime? From = null, [FromQuery] DateTime? To = null)
        {
            if (numberOfResults == 0)
                return await pointsService.GetAllPlayersPoints(onlyActive, From, To);
            else
                return await pointsService.GetAllPlayersPoints(onlyActive, numberOfResults);
        }

        [HttpGet("{id:int}")]
        public async Task<PlayerWithPointsDto> Get(int id, [FromQuery] int numberOfResults = 0, [FromQuery] DateTime? From = null, [FromQuery] DateTime? To = null)
        {
            if (numberOfResults == 0)
                return await pointsService.GetAllPlayersPoints(id, From, To);
            else
                return await pointsService.GetAllPlayersPoints(id, numberOfResults);
        }
    }
}
