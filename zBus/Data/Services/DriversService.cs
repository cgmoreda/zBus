using Microsoft.EntityFrameworkCore;
using zBus.Models;

namespace zBus.Data.Services
{
    public class DriversService : IDriversService
    {
        private readonly AppDbContext _context;

        public DriversService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(Driver driver)
        {
            _context.Drivers.Add(driver);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Driver>> GetAll()
        {
            var drivers = await _context.Drivers.ToListAsync();
            return drivers;
        }

        public Driver GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, Driver driver)
        {
            throw new NotImplementedException();
        }
    }
}
