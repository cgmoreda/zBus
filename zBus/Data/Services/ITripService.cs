using zBus.Models;

namespace zBus.Data.Services
{
    public interface ITripService
    {
        Task<IEnumerable<Trip>> GetAll();
        Task<IEnumerable<Trip>> Search(int id1 ,int id2, DateTime dt);
        Trip GetById(int id);
        void Add(Trip Trip);
        void Update(int id, Trip Trip);
        void Delete(int id);
    }
}
