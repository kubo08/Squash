using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Squash.Domain;

namespace Squash.Data
{
    public class SquashContext : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Points> Points { get; set; }
        public DbSet<PlayerTournamentResult> PlayerTournamentResults { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<Info> Info { get; set; }

        public static readonly ILoggerFactory ConsoleLoggerFactory
            = LoggerFactory.Create(builder =>
            {
                builder.AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information)
                .AddConsole();
            });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLoggerFactory(ConsoleLoggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data Source = localhost; Initial Catalog = Squash; integrated security = true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlayerTournament>().HasKey(p => new { p.PlayerId, p.TournamentId });
        }
    }
}
