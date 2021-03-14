using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SimpleLeaderboard.Client
{
    public class LeaderboardDto
    {
        public LeaderboardDto() {}
        public int LeaderboardId { get; set; }
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string AdminId { get; set; }
        public IEnumerable<LeaderboardPlayerDto> LeaderboardPlayers { get; set; }

    }
}