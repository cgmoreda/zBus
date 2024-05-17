using zBus.Models;

namespace zBus.Data.Services
{
    public interface IBusService
    {
        Task<IEnumerable<Bus>> GetAll();
        Bus GetById(int id);
        void Add(Bus driver);
        void Update(int id, Bus bus);
        bool Delete(int id);
    }
}
