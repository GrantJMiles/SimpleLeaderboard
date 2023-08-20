namespace SimpleLeaderboard.Client
{
    public class LeaderboardPlayerDto
    {
        public LeaderboardPlayerDto() {}
        public int LeaderboardPlayerId { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public string UniqueId { get; set; }
        public double Score { get; set; }

    }
}