using Microsoft.AspNetCore.Mvc;
using Squash.Data.Interfaces;
using Squash.Shared.Data.Dto;
using System.Threading.Tasks;

namespace Squash.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeadToHeadController: ControllerBase
    {
        private IPlayerService playerService;
        public HeadToHeadController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet]
        public Task<HeadToHeadDto> Get([FromQuery] int player1, [FromQuery] int player2,[FromQuery] bool onlyEleven)
        {
            return playerService.GetHeadToHead(player1, player2,onlyEleven);
        }
    }
}
