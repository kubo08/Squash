using Microsoft.AspNetCore.Mvc;
using Squash.Data.Interfaces;
using Squash.Domain;
using Squash.Shared.Data.Dto;
using System.Threading.Tasks;

namespace Squash.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerStatisticsController : ControllerBase
    {
        private IPlayerService playerService;
        public PlayerStatisticsController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet("{id}")]
        public Task<PlayerStatisticsDto> Get(int Id, [FromQuery] bool onlyEleven = true)
        {
            //return playerService.GetPlayerById(Id);
            return playerService.GetPlayerStatistics(Id, onlyEleven);
        }
    }
}
