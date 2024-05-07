using zBus.Models;

namespace zBus.Data.Services
{
    public interface IStationService
    {
        Task<IEnumerable<Station>> GetAll();
        Station GetById(int id);
        void Add(Station station);
        void Update(int id, Station station);
        void Delete(int id);
    }
}
