using zBus.Models;

namespace zBus.Data.Services
{
    public interface ITripService
    {
        Task<IEnumerable<Trip>> GetAll();
        Trip GetById(int id);
        void Add(Trip Trip);
        void Update(int id, Trip Trip);
        void Delete(int id);
    }
}
