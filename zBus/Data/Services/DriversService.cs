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
            var _driver = GetById(id);
            _context.Drivers.Remove(_driver!);
            _context.SaveChanges();
        }

        public List<Driver> GetAll()
        {
            var drivers =  _context.Drivers.ToList();
            return drivers;
        }

        public Driver GetById(int id)
        {
            return _context.Drivers.FirstOrDefault(x => x.DriverId == id)!;
        }

        public void Update(int id, Driver _driver)
        {
            _driver.DriverId = id;
            _context.Drivers.Update(_driver);
            _context.SaveChanges();
        }
    }
}
