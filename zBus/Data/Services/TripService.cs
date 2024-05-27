using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Identity;
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
            try { _context.SaveChanges(); }
            catch { }
        }

        public void Delete(int id)
        {
            var _trip = GetById(id);
            var res = _context.Seats.Where(x => x.TripId == id).ToList();
            foreach (var item in res)
            {
                _context.Seats.Remove(item);
            }
            _context.Trips.Remove(_trip!);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Trip>> GetAll()
        {
            return await _context.Trips
            .Include(s => s.Seats)
            .Include(s => s.ArrivalStation)
            .Include(s => s.DepartureStation)
            .Include(b => b.Bus)
            .Include(u => u.OrderItems)
            .ToListAsync();
        }



        public async Task<IEnumerable<Trip>> Search(int id1, int id2, DateTime dt)
        {
            var trips=new List<Trip>();
            bool check1 = false;
            bool check2 = false;
            //bool check1 = true;
            if (id1!=0)
            {
                check1 = true;
                trips = await _context.Trips.Where(t => t.DepartureStationID == id1).Include(s => s.Seats)
            .Include(s => s.ArrivalStation)
            .Include(s => s.DepartureStation)
            .Include(b => b.Bus)
            .Include(u => u.OrderItems)
            .ToListAsync();
            }
            if(id2!=0)
            {
                check2 = true;
                if(check1)
                {
                    trips = trips.Where(t => t.ArrivalStationID==id2).ToList();
                }
                else
                {
                    trips = await _context.Trips.Where(t => t.ArrivalStationID == id2).Include(s => s.Seats)
            .Include(s => s.ArrivalStation)
            .Include(s => s.DepartureStation)
            .Include(b => b.Bus)
            .Include(u => u.OrderItems)
            .ToListAsync();
                }
            }

            if( dt!= DateTime.MinValue)
            {
                if (check1 || check2) {
                    trips = trips.Where(t => t.DepartureTime.Date == dt).ToList();
                }
                else  { trips = await _context.Trips.Where(t => t.DepartureTime.Date == dt).Include(s => s.Seats)
            .Include(s => s.ArrivalStation)
            .Include(s => s.DepartureStation)
            .Include(b => b.Bus)
            .Include(u => u.OrderItems)
            .ToListAsync();
                }   
            }

            return trips;
        }
        public Trip GetById(int id)
        {
            return _context.Trips
            .Include(s => s.Seats)
            .Include(s => s.ArrivalStation)
            .Include(s => s.DepartureStation)
            .Include(b => b.Bus)
            .Include(u => u.OrderItems)
            .FirstOrDefault(t => t.TripId == id)?? new Trip();
        }
        public void Update(int id, Trip _trip)
        {
             _trip.TripId = id;
            _context.Trips.Update(_trip);
            _context.SaveChanges();
        }
    }
}
