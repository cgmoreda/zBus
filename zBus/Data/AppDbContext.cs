using Microsoft.EntityFrameworkCore;
using zBus.Models
namespace zBus.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        }
        public DbSet<Bus> Buses;
        public DbSet<Driver> Drivers;
        public DbSet<Station> Stations;
        public DbSet<Trip> Trips;
    }
}
