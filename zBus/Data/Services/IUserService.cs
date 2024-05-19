using zBus.Models;

namespace zBus.Data.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        User GetById(string email);
        void Add(User user);
        void Update(string email, User user);
        void Delete(string name);
        bool Exist(string email);
        void Update_Pass(string password ,string email);
    }
}
