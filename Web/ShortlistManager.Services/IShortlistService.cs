using System.Collections.Generic;
using ShortlistManager.Services.Models;

namespace ShortlistManager.Services
{
    public interface IShortlistService
    {
        PlayerDto GetPlayer(int scoutId, int id);
        IEnumerable<PlayerDto> PlayersForScout(int scoutId);
        void AddPlayer(int scoutId, PlayerDto newPlayer);
        void RemovePlayer(int scoutId, int playerId);
    }
}