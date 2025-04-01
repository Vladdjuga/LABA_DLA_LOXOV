using LAB02_DLL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter.Xml;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
        public DbSet<DOTACharacter> DOTACharacters { get; set; }

        public EntityEntry<TEntity> Submit<TEntity>(TEntity entity) where TEntity : class
        {
            Attach(entity)
                .State = (int)(entity.GetType().GetProperty("Id").GetValue(entity)) == 0 ? EntityState.Added : EntityState.Modified;
            return Entry<TEntity>(entity);
        }

        public EntityEntry<TEntity1> AttachConnection<TEntity1, TEntity2>(TEntity1 entity, List<TEntity2> ids,PropertyInfo pr) where TEntity1 : class
        {
            pr.SetValue(entity,ids);
            return Entry<TEntity1>(entity);
        }

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
                        .HasOne(a => a.Author)
                        .WithMany(a => a.Articles)
                        .HasForeignKey(e => e.AuthorId)
                        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<Article>()
                        .HasOne(a => a.Category)
                        .WithMany(c => c.Articles)
                        .HasForeignKey(e => e.CategoryId)
                        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<MatchEvent>()
                .HasOne(m => m.Match)
                        .WithMany(m => m.MatchEvents)
                        .HasForeignKey(e => e.MatchId)
                        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DOTACharacter>()
                        .HasOne(d=>d.Position)
                        .WithMany(p=>p.DOTACharacters)
                        .HasForeignKey(e=>e.PositionId)
                        .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<DOTACharacter>()
                .HasMany(d=>d.Players)
                        .WithMany(p=>p.DOTACharacters);
        }
    }
}
