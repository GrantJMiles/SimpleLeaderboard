using System.Collections.Generic;
using System.Linq;
namespace SimpleLeaderboard.Client
{
    public class LeaderboardEventDto
    {
        public LeaderboardEventDto() {}
        public int LeaderboardEventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UniqueId { get; set; }
        public string AdminId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDescending { get; set; }
        IEnumerable<LeaderboardDto> Leaderboards { get; set; }
    }
}