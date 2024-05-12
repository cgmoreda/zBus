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

        public async Task<IEnumerable<Trip>> GetAll()=> await _context.Trips.ToListAsync();



        public async Task<IEnumerable<Trip>> Search(int id1, int id2, DateTime dt)
        {
            var trips=new List<Trip>();
            bool check1 = false;
            bool check2 = false;
            //bool check1 = true;
            if (id1!=0)
            {
                check1 = true;
                trips = await _context.Trips.Where(t => t.DepartureStationID == id1).ToListAsync();
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
                    trips = await _context.Trips.Where(t => t.ArrivalStationID == id2).ToListAsync();
                }
            }

            if( dt!= DateTime.MinValue)
            {
                if (check1 || check2) {
                    trips = trips.Where(t => t.DepartureTime.Date == dt).ToList();
                }
                else  { trips = await _context.Trips.Where(t => t.DepartureTime.Date == dt).ToListAsync(); }   
            }

            return trips;
        }
        public Trip GetById(int id) => _context.Trips.FirstOrDefault(x => x.TripId == id)!;

        public void Update(int id, Trip _trip)
        {
            var old=GetById(id);
            old.DepartureStationID = _trip.DepartureStationID;
            old.DepartureStation= _trip.DepartureStation;
            old.ArrivalStationID = _trip.ArrivalStationID;
            old.ArrivalStation= _trip.ArrivalStation;
            old.Bus= _trip.Bus;
            old.BusId= _trip.BusId;
            old.DepartureTime= _trip.DepartureTime;
            old.ArrivalTime = _trip.ArrivalTime;
            old.TripPrice= _trip.TripPrice;
            _context.Trips.Update(old);
            _context.SaveChanges();
        }
    }
}
