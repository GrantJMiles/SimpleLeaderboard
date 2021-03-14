using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using SimpleLeaderboard.Domain;
using System;

namespace SimpleLeaderboard.Domain
{
    public interface ISimpleLeaderboardWrite
    {
        Task<LeaderboardEventDto> CreateEvent(string title, string description);
        Task<LeaderboardDto> AddLeaderBoard(int LeaderboardEventId, string leaderboardTtitle, IEnumerable<LeaderboardPlayer> players);
        Task<IEnumerable<LeaderboardPlayer>> AddPlayer(int leaderboardId, IEnumerable<LeaderboardPlayer> newPlayers);
        Task<LeaderboardPlayerDto> UpdatePlayerScore(int leaderboardId, int playerId, int playerScore);
        Task<LeaderboardEventDto> EndEvent(int leaderboardEventId);
    }
}