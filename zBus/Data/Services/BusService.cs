using Microsoft.EntityFrameworkCore;
using zBus.Models;

namespace zBus.Data.Services
{
    public class BusService : IBusService
    {
        private readonly AppDbContext _context;

        public BusService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Bus _bus)
        {
            _context.Buses.Add(_bus);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var _bus = GetById(id);
            _context.Buses.Remove(_bus!);
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Bus>> GetAll()
        {
            var Buss = await _context.Buses.ToListAsync();
            return Buss;
        }

        public Bus GetById(int id)
        {
            return _context.Buses.FirstOrDefault(x => x.BusId == id)!;
        }

        public void Update(int id, Bus _bus)
        {
            _bus.BusId = id;
            _context.Buses.Update(_bus);
            _context.SaveChanges();
        }
    }
}
