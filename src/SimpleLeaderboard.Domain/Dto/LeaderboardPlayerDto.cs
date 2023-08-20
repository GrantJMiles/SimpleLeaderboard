namespace SimpleLeaderboard.Domain
{
    public class LeaderboardPlayerDto
    {
        public LeaderboardPlayerDto() {}
        public LeaderboardPlayerDto(LeaderboardPlayer entity) {
            LeaderboardPlayerId = entity.LeaderboardPlayerId;
            Name = entity.Name;
            Nickname = entity.Nickname;
            UniqueId = entity.UniqueId;
            Score = entity.Score;
        }
        public int LeaderboardPlayerId { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string UniqueId { get; set; }
        public double Score { get; set; }


        public static LeaderboardPlayerDto Create(LeaderboardPlayer entity) => new LeaderboardPlayerDto(entity);
    }
}