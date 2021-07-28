using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WearMatcher.Models;

namespace WearMatcher.Data
{
    public class WearMatcherContext : DbContext
    {
        public WearMatcherContext (DbContextOptions<WearMatcherContext> options)
            : base(options)
        {
        }

        public DbSet<WearMatcher.Models.ClothingItem> ClothingItem { get; set; }
        public DbSet<WearMatcher.Models.Tag> Tag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ClothingItem>()
                .HasMany(i => i.MatchingItems)
                .WithMany(i => i.MatchingItems)
                .UsingEntity<MatchingItemsPair>(
                    j => j
                        .HasOne(ip => ip.FirstItem)
                        .WithMany(i => i.ItemItem)
                        .HasForeignKey(ip => ip.FirstItemId),
                    j => j
                        .HasOne(ip => ip.FirstItem)
                        .WithMany(i => i.ItemItem)
                        .HasForeignKey(ip => ip.FirstItemId),
                    j => j.HasKey(t => new { t.FirstItemId, t.SecondItemId })
                );
        }
    }
}
