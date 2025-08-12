using Microsoft.EntityFrameworkCore;
using Task_Progress_Generate_HTTP_Links.Models;

namespace Task_Progress_Generate_HTTP_Links.Data
{
    public class UrlDbContext : DbContext
    {
        public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options) { }

        public DbSet<Url> Urls { get; set; }
        public DbSet<Visit> Visits { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>()
                .HasIndex(u => u.ShortlUrl)
                .IsUnique();

            modelBuilder.Entity<Url>()
                .HasIndex(u => u.SecretURL)
                .IsUnique();

            modelBuilder.Entity<Url>()
                .Property(u => u.OriginalUrl)
                .HasMaxLength(2048);

            modelBuilder.Entity<Visit>()
                .Property(v => v.IpAddress)
                .HasMaxLength(45);
        }
    }
}
