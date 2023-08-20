using System.Collections.Generic;

namespace SimpleLeaderboard.Client
{
    public class NewLeaderboard
    {
        public int LeaderboardEventId { get; set; }
        public string Title { get; set; }
        public string UniqueId { get; set; }
        public string AdminId { get; set; }
        public List<NewPlayer> Players { get; set; }
    }
}