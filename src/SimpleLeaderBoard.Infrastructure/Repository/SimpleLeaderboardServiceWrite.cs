using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SimpleLeaderboard.Domain;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLeaderboard.Infrastructure
{
    public class SimpleLeaderboardServiceWrite : ISimpleLeaderboardWrite
    {
        private readonly SimpleLeaderboardContext _db;
        public SimpleLeaderboardServiceWrite(SimpleLeaderboardContext db) 
        {
            _db = db ?? throw new ArgumentException(nameof(SimpleLeaderboardContext));
        }

        public async Task<LeaderboardDto> AddLeaderBoard(int LeaderboardEventId, string leaderboardTitle, IEnumerable<LeaderboardPlayer> players, string uniqueId = null, string adminId = null)
        {
            var leaderboard = await _db.LeaderboardEvents.FirstOrDefaultAsync(f => f.LeaderboardEventId == LeaderboardEventId) ?? throw new ArgumentException("invalid leaderboard id");
            var newEntity = new Leaderboard {
                AdminId = string.IsNullOrWhiteSpace(adminId) ? Guid.NewGuid().ToString() : adminId,
                LeaderboardEvent = leaderboard,
                Title = leaderboardTitle,
                UniqueId = string.IsNullOrWhiteSpace(uniqueId) ? Guid.NewGuid().ToString() : uniqueId
            };
            await _db.Leaderboards.AddAsync(newEntity);
            await _db.SaveChangesAsync();
            var newPlayers = await AddPlayer(newEntity.LeaderboardId, players);
            newEntity.LeaderboardPlayers = newPlayers.ToList();
            return LeaderboardDto.Create(newEntity, true);
        }

        public async Task<IEnumerable<LeaderboardPlayer>> AddPlayer(int leaderboardId, IEnumerable<LeaderboardPlayer> newPlayers)
        {
            var leaderboard = await _db.Leaderboards.FirstOrDefaultAsync(f => f.LeaderboardId == leaderboardId);
            var newEntities = new List<LeaderboardPlayer>();
            foreach(var player in newPlayers ?? new List<LeaderboardPlayer>()) {
                if (! (string.IsNullOrEmpty(player.Name) && string.IsNullOrEmpty(player.Nickname))) 
                {
                    var newEntity = new LeaderboardPlayer {
                        Leaderboard = leaderboard,
                        Name = player.Name,
                        Nickname = player.Nickname,
                        Score = 0,
                        UniqueId = Guid.NewGuid().ToString()
                    };
                    newEntities.Add(newEntity);
                }
            }
            await _db.LeaderboardPlayers.AddRangeAsync(newEntities);
            await _db.SaveChangesAsync();
            return newEntities.OrderByDescending(o => o.Score);
        }

        public async Task<LeaderboardEventDto> CreateEvent(string title, string description, bool isDescending, string uniqueId = null, string adminId = null)
        {
            var newEntity = new LeaderboardEvent {
                AdminId = string.IsNullOrWhiteSpace(adminId) ? Guid.NewGuid().ToString() : adminId,
                Description = description,
                IsActive = true,
                Title = title,
                IsDescending = isDescending,
                UniqueId = string.IsNullOrWhiteSpace(uniqueId) ? Guid.NewGuid().ToString() : uniqueId
            };
            await _db.LeaderboardEvents.AddAsync(newEntity);
            await _db.SaveChangesAsync();
            return LeaderboardEventDto.Create(newEntity);
        }

        public async Task<LeaderboardEventDto> EndEvent(int leaderboardEventId)
        {
            var leaderboardEvent = await _db.LeaderboardEvents.FirstOrDefaultAsync(f => f.LeaderboardEventId == leaderboardEventId) ?? throw new ArgumentException("invalid event ID");
            leaderboardEvent.IsActive = false;
            await _db.SaveChangesAsync();
            return LeaderboardEventDto.Create(leaderboardEvent);
        }

        public async Task<LeaderboardPlayerDto> UpdatePlayerScore(int leaderboardId, int playerId, double playerScore)
        {
            var player = await _db.LeaderboardPlayers.FirstOrDefaultAsync(f => f.LeaderboardPlayerId == playerId) ?? throw new ArgumentException("invalid player id");
            player.Score = playerScore;
            await _db.SaveChangesAsync();

            return LeaderboardPlayerDto.Create(player);
        }
    }
}