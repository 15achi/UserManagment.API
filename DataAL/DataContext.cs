
using DataAL.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Country> Countries { get; set; }
        protected override void OnModelCreating(ModelBuilder Builder)
        {
            Builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            Builder.Entity<User>()
                .HasIndex(u => u.PrivateNumber)
                .IsUnique();
            Builder.Entity<User>()
                .HasIndex(u => u.Phone)
                .IsUnique();
        }
    }
}