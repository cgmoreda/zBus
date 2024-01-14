using Microsoft.EntityFrameworkCore;
using zBus.Models;
namespace zBus.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>()
                .HasOne(t => t.DepartureStation)
                .WithMany()
                .HasForeignKey(t => t.DepartureCityID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.ArrivalStation)
                .WithMany()
                .HasForeignKey(t => t.ArrivalCityID)
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Trip> Trips { get; set; }
    }
}
