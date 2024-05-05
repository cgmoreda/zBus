using Microsoft.EntityFrameworkCore;
using zBus.Models;

namespace zBus.Data.Services
{
    public class SeatService : ISeatService
    {
        private readonly AppDbContext _context;

        public SeatService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Seat seat)
        {
            _context.Seats.Add(seat);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Seat>> GetAll()
        {
            var seats = await _context.Seats.ToListAsync();
            return seats;
        }

        public Seat GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Seat seat)
        {
            throw new NotImplementedException();
        }
    }
}
