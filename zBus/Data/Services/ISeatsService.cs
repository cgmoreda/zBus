using zBus.Models;

namespace zBus.Data.Services
{
    public interface ISeatsService
    {
        Task<IEnumerable<Seat>> GetAll();
        List<Seat> GetById(int id);
        void Add(Seat Seat);
        void Update(int id,List<int> ids);
        void Delete(int id);
    }
}
