using Microsoft.EntityFrameworkCore;
using zBus.Models;

namespace zBus.Data.Services
{
    public class BussService : IBusService
    {
        private readonly AppDbContext _context;

        public BussService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Bus driver)
        {
            _context.Buses.Add(driver);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Bus>> GetAll()
        {
            var Buss = await _context.Buses.ToListAsync();
            return Buss;
        }

        public Bus GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Bus bus)
        {
            throw new NotImplementedException();
        }
    }
}
