using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // Tables that will be created, uses Domain entitites
        public DbSet<Activity> Activities { get; set; }
    }
}