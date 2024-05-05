using zBus.Models;

namespace zBus.Data.Services
{
    public interface IStationService
    {
        Task<IEnumerable<Station>> GetAll();
        Station GetById(int id);
        void Add(Station driver);
        void Update(int id, Station driver);
        void Delete(int id);
    }
}
