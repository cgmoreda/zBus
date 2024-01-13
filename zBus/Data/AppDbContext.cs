using Microsoft.EntityFrameworkCore;
using zBus.Models;
namespace zBus.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        }

        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Trip> Trips { get; set; }
    }
}
