using System.Collections.Generic;
namespace SimpleLeaderboard.Domain
{
    public interface ILeaderboardEvent
    {
        int LeaderboardEventId { get; set; }
        string Title { get; set; }
        string Description { get; set; }
        string UniqueId { get; set; }
        string AdminId { get; set; }
        bool IsActive { get; set; }
    }
}