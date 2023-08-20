using System.Collections.Generic;

namespace SimpleLeaderboard.Domain
{
    public class CreateLeaderboardDto
    {
        public int LeaderboardEventId { get; set; }
        public string Title { get; set; }
        public string UniqueId { get; set; }
        public IEnumerable<NewPlayerDto> Players { get; set; }
    }
}