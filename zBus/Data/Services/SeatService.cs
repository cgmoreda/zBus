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
        public void Add(Seat _seat)
        {
            _context.Seats.Add(_seat);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var _seat = GetById(id);
            _context.Seats.Remove(_seat!);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Seat>> GetAll()
        {
            var seats = await _context.Seats.ToListAsync();
            return seats;
        }

        public Seat GetById(int id)
        {
            return _context.Seats.FirstOrDefault(x => x.SeatId == id)!;
        }

        public void Update(int id, Seat _seat)
        {
         
            _seat.SeatId = id;
            _context.Seats.Update(_seat);
            _context.SaveChanges();
        }
    }
}
