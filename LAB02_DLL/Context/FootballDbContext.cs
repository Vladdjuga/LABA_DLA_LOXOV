using LAB02_DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02_DLL.Context
{
    public class FootballDbContext(DbContextOptions<FootballDbContext> dbContextOptions) : DbContext(dbContextOptions)
    {
        public DbSet<Author> Authors { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchEvent> MatchEvents { get; set; }
        public DbSet<MatchPlayer> MatchPlayers { get; set; }
        public DbSet<EventType> EventTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MatchPlayer>()
                .HasKey(p => new { p.Id, p.PlayerId });
            modelBuilder.Entity<Match>()
                        .HasOne(m => m.HomeTeam)
                        .WithMany(t => t.HomeMatches)
                        .HasForeignKey(e=>e.HomeTeamId)
                        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Match>()
                        .HasOne(m => m.AwayTeam)
                        .WithMany(t => t.AwayMatches)
                        .HasForeignKey(e=>e.AwayTeamId)
                        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Article>()
                        .HasOne(a=>a.Author)
                        .WithMany(a=>a.Articles)
                        .HasForeignKey(e=>e.AuthorId)
                        .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
