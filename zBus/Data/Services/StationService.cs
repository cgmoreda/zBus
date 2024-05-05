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
        public void Add(Station station)
        {
            _context.Stations.Add(station);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Station>> GetAll()
        {
            var stations = await _context.Stations.ToListAsync();
            return stations;
        }

        public Station GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Station station)
        {
            throw new NotImplementedException();
        }
    }
}
