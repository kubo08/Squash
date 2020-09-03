using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Squash.Data.Interfaces;
using Squash.Domain;
using Squash.Shared.Data.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Squash.Web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlayerController : ControllerBase
    {
        private IPlayerService playerService;
        public PlayerController(IPlayerService playerService)
        {
            this.playerService = playerService;
        }

        [HttpGet]
        public IEnumerable<Player> Get([FromHeader] bool onlyActive=true)
        {
            return playerService.GetAllPlayers(onlyActive);
        }

        [HttpGet("{id}")]
        public Player Get(int Id)
        {
            return playerService.GetPlayerById(Id);
        }
    }
}
