using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using SimpleLeaderboard.Domain;
using System;

namespace SimpleLeaderboard.Domain
{
    public interface ISimpleLeaderboardRead
    {
        Task<IEnumerable<LeaderboardEventDto>> GetLeaderboardEvents(Expression<Func<LeaderboardEvent,bool>> filter = null);
        Task<LeaderboardEventDto> GetLeaderboardEvent(int id);
        Task<IEnumerable<LeaderboardDto>> GetLeaderboards(Expression<Func<Leaderboard,bool>> filter = null);
        Task<LeaderboardDto> GetLeaderboard(int id);
        Task<IEnumerable<LeaderboardPlayerDto>> GetLeaderboardPlayers(Expression<Func<LeaderboardPlayer,bool>> filter = null);
        Task<LeaderboardPlayerDto> GetLeaderboardPlayer(int id);
    }
}