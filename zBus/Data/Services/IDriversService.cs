using zBus.Models;

namespace zBus.Data.Services
{
    public interface IDriversService
    {
        Task<IEnumerable<Driver>> GetAll();
        Driver GetById(int id);
        void Add(Driver driver);
        void Update(int id, Driver driver);
        bool Delete(int id);
    }
}
