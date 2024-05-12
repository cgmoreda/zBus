using zBus.Models;

namespace zBus.Data.Services
{
    public interface ISeatsService
    {
        Task<IEnumerable<Seat>> GetAll();
        Seat GetById(int id);
        void Add(Seat Seat);
        void Update(int id, Seat Seat);
        void Delete(int id);
    }
}
