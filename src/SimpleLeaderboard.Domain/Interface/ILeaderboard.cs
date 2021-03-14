using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleLeaderboard.Domain
{
    public interface ILeaderboard
    {
        int LeaderboardId { get; set; }
        string UniqueId { get; set; }
        string Title { get; set; }
        string AdminId { get; set; }

        ICollection<ILeaderboardPlayer> LeaderboardPlayers { get; set; }
    }
}