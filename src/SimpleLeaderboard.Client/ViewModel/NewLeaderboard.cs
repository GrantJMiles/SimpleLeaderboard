using System.Collections.Generic;

namespace SimpleLeaderboard.Client
{
    public class NewLeaderboard
    {
        public int LeaderboardEventId { get; set; }
        public string Title { get; set; }
        public List<NewPlayer> Players { get; set; }
    }
}