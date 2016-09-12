using System;
using System.Collections.Generic;
using System.Linq;
using ShortlistManager.Services.Db.Entities;
using ShortlistManager.Services.Models;
using ShortlistManager.Services.Models.Mapping;

namespace ShortlistManager.Services
{
    public class ShortlistService : IShortlistService
    {
        private readonly IDbContext _db;
        private readonly IMapper<Player, PlayerDto> _outMapper;

        public ShortlistService( IDbContext db, IMapper<Player, PlayerDto> outMapper)
        {
            _db = db;
            _outMapper = outMapper;
        }

        public PlayerDto GetPlayer(int scoutId, int id)
        {
            var player = _db.Players.SingleOrDefault(x => x.Id == id);

            if (player == null)
                return null;

            if (player.ScoutId != scoutId)
                throw new ApplicationException($"Player '{id}' does not belong to Scout '{scoutId}'.");

            return _outMapper.Create(player);
        }

        public IEnumerable<PlayerDto> PlayersForScout(int scoutId)
        {
            var scout = _db.Scouts.SingleOrDefault(x => x.Id == scoutId);

            if (scout == null)
                throw new ApplicationException($"Scout '{scoutId}' does not exist.");

            return scout.Players.Select(player => _outMapper.Create(player));
        }

        public void AddPlayer(int scoutId, PlayerDto newPlayer)
        {
            Player targetPlayer;

            if (newPlayer.Id == 0)
            {
                targetPlayer = new Player {ScoutId = scoutId};
                _db.Players.Add(targetPlayer);
            }
            else
            {
                targetPlayer = _db.Players.Single(x => x.Id == newPlayer.Id);
            }

            // move this out of the 'else' to avoid ot getting broken in the 'if'
            if (targetPlayer.ScoutId != scoutId)
                throw new ApplicationException($"Player '{targetPlayer.Id}' does not belong to scout '{scoutId}'.");

            // Explicitly set properties inbound
            targetPlayer.FirstName = newPlayer.FirstName;
            targetPlayer.Surname = newPlayer.Surname;
            targetPlayer.DateOfBirth = newPlayer.DateOfBirth;

            if (!string.IsNullOrWhiteSpace(newPlayer.ClubName))
            {
                var club = GetClub(newPlayer.ClubName);
                targetPlayer.ClubId = club.Id;
            }

            _db.SaveChanges();
        }

        public void RemovePlayer(int scoutId, int playerId)
        {
            var player = _db.Players.SingleOrDefault(x => x.Id == playerId);

            if(player == null)
                throw new ApplicationException($"Player '{playerId}' does not exist.");

            if (player.Scout.Id != scoutId)
                throw new ApplicationException($"Player '{playerId}' does not belong to scout '{scoutId}'.");

            _db.Players.Remove(player);
            _db.SaveChanges();
        }

        private Club GetClub(string clubName)
        {
            var loweredClubName = clubName.ToLower();
            var club = _db.Clubs.SingleOrDefault(x => x.Name.ToLower() == loweredClubName);

            if (club == null)
            {
                club = new Club();
                club.Name = clubName;
                _db.Clubs.Add(club);
            }

            return club;
        }
    }
}
