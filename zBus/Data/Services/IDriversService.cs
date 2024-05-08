using zBus.Models;

namespace zBus.Data.Services
{
    public interface IDriversService
    {
        List<Driver> GetAll();
        Driver GetById(int id);
        void Add(Driver driver);
        void Update(int id, Driver driver);
        void Delete(int id);
    }
}
