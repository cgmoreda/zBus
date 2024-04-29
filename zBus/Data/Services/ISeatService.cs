using zBus.Models;

namespace zBus.Data.Services
{
    public interface ISeatService
    {
        Task<IEnumerable<Seat>> GetAll();
        Seat GetById(int id);
        void Add(Seat driver);
        void Update(int id, Seat driver);
        void Delete(int id);
    }
}
