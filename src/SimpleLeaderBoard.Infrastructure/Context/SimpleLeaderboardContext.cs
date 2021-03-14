using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SimpleLeaderboard.Domain;

namespace SimpleLeaderboard.Infrastructure
{
    public class SimpleLeaderboardContext : DbContext
    {
        public SimpleLeaderboardContext(DbContextOptions<SimpleLeaderboardContext> context):base(context){}
        public DbSet<Leaderboard> Leaderboards {get; set;}
        public DbSet<LeaderboardEvent> LeaderboardEvents {get; set;}
        public DbSet<LeaderboardPlayer> LeaderboardPlayers {get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {

        }
    }
}