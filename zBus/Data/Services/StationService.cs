using Microsoft.EntityFrameworkCore;
using zBus.Models;
using static System.Collections.Specialized.BitVector32;

namespace zBus.Data.Services
{
    public class StationService : IStationService
    {
        private readonly AppDbContext _context;

        public StationService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Station _station)
        {
            _context.Stations.Add(_station);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var _station = GetById(id);
            _context.Stations.Remove(_station!);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Station>> GetAll()
        {
            var stations = await _context.Stations.ToListAsync();
            return stations;
        }

        public Station GetById(int id)
        {
            return _context.Stations.FirstOrDefault(x => x.StationId == id)!;
        }

        public void Update(int id, Station _station)
        {
            var old=_context.Stations.FirstOrDefault(x => x.StationId == id)!;
            old.StationCity = _station.StationCity;
            old.StationAddress = _station.StationAddress;
            old.PhotoUrl = _station.PhotoUrl;
            old.ContactNumber = _station.ContactNumber;
            old.StationName = _station.StationName;
            _context.Stations.Update(old);
            _context.SaveChanges();
        }
    }
}
