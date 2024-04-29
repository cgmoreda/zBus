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
        public void Add(Trip seat)
        {
            _context.Trips.Add(seat);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Trip>> GetAll()
        {
            var seats = await _context.Trips.ToListAsync();
            return seats;
        }

        public Trip GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Trip seat)
        {
            throw new NotImplementedException();
        }
    }
}
