using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SimpleLeaderboard.Domain
{
    public class Leaderboard
    {
        [Required]
        public int LeaderboardId { get; set; }
        [Required]
        public string UniqueId { get; set; }
        [Required]
        public string Title { get; set; }
        public string AdminId { get; set; }
        

        public LeaderboardEvent LeaderboardEvent { get; set; }
        public ICollection<LeaderboardPlayer> LeaderboardPlayers { get; set; }
    }
}