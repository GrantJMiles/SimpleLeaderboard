using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SimpleLeaderboard.Domain;
using System.Linq.Expressions;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleLeaderboard.Infrastructure
{
    public class SimpleLeaderboardServiceRead : ISimpleLeaderboardRead
    {
        private readonly SimpleLeaderboardContext _db;
        public SimpleLeaderboardServiceRead(SimpleLeaderboardContext db) 
        {
            _db = db ?? throw new ArgumentException(nameof(SimpleLeaderboardContext));
        }

        public async Task<IEnumerable<LeaderboardEventDto>> GetLeaderboardEvents(Expression<Func<LeaderboardEvent,bool>> filter = null)
        {
            var events = _db.LeaderboardEvents.AsQueryable();
            events = filter == null ? events : events.Where(filter);
            return await events.Select(s => LeaderboardEventDto.Create(s)).ToListAsync();
        }
        public async Task<LeaderboardEventDto> GetLeaderboardEvent(int id) 
        {
            return await _db.LeaderboardEvents
                .Select(s => LeaderboardEventDto.Create(s))
                .FirstOrDefaultAsync(f => f.LeaderboardEventId == id);
        }

        public async Task<IEnumerable<LeaderboardDto>> GetLeaderboards(Expression<Func<Leaderboard,bool>> filter = null)
        {
            var events = _db.Leaderboards
            .AsQueryable();
            events = filter == null ? events : events.Where(filter);
            var dto = events
            .Select(s => new LeaderboardDto
            {
                AdminId = s.AdminId,
                LeaderboardId = s.LeaderboardId,
                Title = s.Title,
                UniqueId = s.UniqueId,
                LeaderboardPlayers = s.LeaderboardPlayers.OrderByDescending(o => o.Score).Select(se => LeaderboardPlayerDto.Create(se))
            });
            return await dto.ToListAsync();
        }
        public async Task<LeaderboardDto> GetLeaderboard(int id) 
        {
            return await _db.Leaderboards
                .Select(s => new LeaderboardDto
                {
                    AdminId = s.AdminId,
                    LeaderboardId = s.LeaderboardId,
                    Title = s.Title,
                    UniqueId = s.UniqueId,
                    LeaderboardPlayers = s.LeaderboardPlayers.OrderByDescending(o => o.Score).Select(se => LeaderboardPlayerDto.Create(se))
                })
                .FirstOrDefaultAsync(f => f.LeaderboardId == id);
        }

        public async Task<IEnumerable<LeaderboardPlayerDto>> GetLeaderboardPlayers(Expression<Func<LeaderboardPlayer,bool>> filter = null)
        {
            var events = _db.LeaderboardPlayers.AsQueryable();
            events = filter == null ? events : events.Where(filter);
            return await events.OrderByDescending(o => o.Score).Select(s => LeaderboardPlayerDto.Create(s)).ToListAsync();
        }
        public async Task<LeaderboardPlayerDto> GetLeaderboardPlayer(int id) 
        {
            return await _db.LeaderboardPlayers
                .OrderByDescending(o => o.Score)
                .Select(s => LeaderboardPlayerDto.Create(s))
                .FirstOrDefaultAsync(f => f.LeaderboardPlayerId == id);
        }
    }
}