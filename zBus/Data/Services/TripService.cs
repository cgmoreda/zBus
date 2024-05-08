using Microsoft.EntityFrameworkCore;
using zBus.Models;

namespace zBus.Data.Services
{
    public class TripService : ITripService
    {
        private readonly AppDbContext _context;

        public TripService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Trip trip)
        {
            _context.Trips.Add(trip);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var _trip = GetById(id);
            _context.Trips.Remove(_trip!);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Trip>> GetAll()
        {
            var seats = await _context.Trips.ToListAsync();
            return seats;
        }

        public Trip GetById(int id)
        {
            return _context.Trips.FirstOrDefault(x => x.TripId == id)!;
        }

        public void Update(int id, Trip _trip)
        {
            _trip.TripId = id;
            _context.Trips.Update(_trip);
            _context.SaveChanges();
        }
    }
}
