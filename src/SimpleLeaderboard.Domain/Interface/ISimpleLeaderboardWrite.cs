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
        Task<LeaderboardEventDto> CreateEvent(string title, string description, bool isDescending, string uniqueId = null, string adminId = null);
        Task<LeaderboardDto> AddLeaderBoard(int LeaderboardEventId, string leaderboardTitle, IEnumerable<LeaderboardPlayer> players, string uniqueId = null, string adminId = null);
        Task<IEnumerable<LeaderboardPlayer>> AddPlayer(int leaderboardId, IEnumerable<LeaderboardPlayer> newPlayers);
        Task<LeaderboardPlayerDto> UpdatePlayerScore(int leaderboardId, int playerId, double playerScore);
        Task<LeaderboardEventDto> EndEvent(int leaderboardEventId);
    }
}