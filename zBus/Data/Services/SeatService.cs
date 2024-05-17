using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using zBus.Models;
using zBus.Data.Enums;
namespace zBus.Data.Services
{
    public class SeatService : ISeatsService
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
            var _seat = _context.Seats.FirstOrDefault(s => s.SeatId == id); ;
            _context.Seats.Remove(_seat!);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Seat>> GetAll()
        {
            var seats = await _context.Seats.ToListAsync();
            return seats;
        }

        public List<Seat> GetById(int id)
        {
            return _context.Seats.Where(x => x.TripId == id).ToList()?? new List<Seat>();
        }

        public void Update(int id, List<int> _seat)
        {
          foreach(var item in _seat)
            {
                var seat = _context.Seats.FirstOrDefault(x => x.SeatId == item && x.TripId == id);
                seat.Status = SeatStatus.Booked;
                _context.Seats.Update(seat);
                _context.SaveChanges();

            }
        }
    }
}
