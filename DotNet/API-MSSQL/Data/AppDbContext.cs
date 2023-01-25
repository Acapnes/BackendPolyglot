using API_MSSQL.Models;
using Microsoft.EntityFrameworkCore;

namespace API_MSSQL.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Entry> Entries { get; set; }
    }
}
