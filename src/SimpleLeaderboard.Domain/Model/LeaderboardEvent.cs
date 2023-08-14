using System.Collections.Generic;
namespace SimpleLeaderboard.Domain
{
    public class LeaderboardEvent
    {
        public int LeaderboardEventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UniqueId { get; set; }
        public string AdminId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDescending { get; set; }
        public ICollection<Leaderboard> Leaderboards { get; set; }
    }
}