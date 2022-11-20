using Andals.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Andals.API.Data
{
    public class AndalsDbContext : DbContext
    {
        public AndalsDbContext(DbContextOptions<AndalsDbContext> options) : base(options)
        {
        }

        //DbSet
        public DbSet<Title> Titles { get; set; }
        public DbSet<Position> Positions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Title>()
               .HasMany(a => a.positions)
               .WithOne(a => a.Title);

        }
    }
}
