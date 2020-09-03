using Squash.Data.Interfaces;
using Squash.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Squash.Shared.Data.Dto;

namespace Squash.Web.Shared.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public List<Player> GetAllPlayers(bool onlyActive)
        {
            return _playerRepository.GetAll().Where(a=>onlyActive ? a.IsActive : true).ToList();
        }

        public Player GetPlayerById(int Id)
        {
            return (_playerRepository.Get(a=>a.Id==Id, includeProperties: "Points")).FirstOrDefault(); //_playerRepository.GetPlayerWithStatistics(Id);
        }

        public Task<HeadToHeadDto> GetHeadToHead(int player1Id, int player2Id, bool onlyEleven)
        {
            return (_playerRepository.GetHeadToHead(player1Id, player2Id,onlyEleven));
        }

        public Task<PlayerStatisticsDto> GetPlayerStatistics(int playerId, bool onlyEleven)
        {
            return (_playerRepository.GetPlayerStatistics(playerId, onlyEleven));
        }
    }
}
