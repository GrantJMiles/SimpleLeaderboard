namespace SimpleLeaderboard.Domain
{
    public interface ILeaderboardPlayer
    {
        int LeaderboardPlayerId { get; set; }
        string Name { get; set; }
        string Nickname { get; set; }
        string UniqueId { get; set; }
        int Score { get; set; }
    }
}