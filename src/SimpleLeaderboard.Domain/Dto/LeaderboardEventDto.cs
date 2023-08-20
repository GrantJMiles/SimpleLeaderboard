using System.Collections.Generic;
using System.Linq;
namespace SimpleLeaderboard.Domain
{
    public class LeaderboardEventDto
    {
        public LeaderboardEventDto() {}
        public LeaderboardEventDto(LeaderboardEvent entity, bool includeRelative = false) {
            LeaderboardEventId = entity.LeaderboardEventId;
            Title = entity.Title;
            Description = entity.Description;
            UniqueId = entity.UniqueId;
            AdminId = entity.AdminId;
            IsActive = entity.IsActive;
            IsDescending = entity.IsDescending;
            Leaderboards = includeRelative ? entity.Leaderboards.Select(s => LeaderboardDto.Create(s)) : new List<LeaderboardDto>();
        }
        public int LeaderboardEventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UniqueId { get; set; }
        public string AdminId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDescending { get; set; }
        IEnumerable<LeaderboardDto> Leaderboards { get; set; }

        public static LeaderboardEventDto Create(LeaderboardEvent entity) => new LeaderboardEventDto(entity);
    }
}