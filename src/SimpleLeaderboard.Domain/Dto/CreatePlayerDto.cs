using System.Collections.Generic;

namespace SimpleLeaderboard.Domain
{
    public class CreatePlayerDto
    {
        public int LeaderboardId { get; set; }
        public IEnumerable<NewPlayerDto> Players {get; set;}
    }
}