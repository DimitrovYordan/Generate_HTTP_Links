using Microsoft.EntityFrameworkCore;
using Task_Progress_Generate_HTTP_Links.Models;

namespace Task_Progress_Generate_HTTP_Links.Data
{
    public class UrlDbContext : DbContext
    {
        public UrlDbContext(DbContextOptions<UrlDbContext> options) : base(options) { }

        public DbSet<Url> Urls { get; set; }
        public DbSet<Visit> Visits { get; set; }
    }
}
