using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SimpleLeaderboard.Domain
{
    public class LeaderboardDto
    {
        public LeaderboardDto() {}
        public LeaderboardDto(Leaderboard entity, bool includeRelative = false)
        {
            LeaderboardId = entity.LeaderboardId;
            UniqueId = entity.UniqueId;
            Title = entity.Title;
            AdminId = entity.AdminId;
            LeaderboardPlayers =  includeRelative ? entity.LeaderboardPlayers.Select(s => LeaderboardPlayerDto.Create(s)) : new List<LeaderboardPlayerDto>();
        }
        public int LeaderboardId { get; set; }
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string AdminId { get; set; }
        public IEnumerable<LeaderboardPlayerDto> LeaderboardPlayers { get; set; }

        public static LeaderboardDto Create(Leaderboard entity, bool includeRelative = false) => new LeaderboardDto(entity, includeRelative);
    }
}