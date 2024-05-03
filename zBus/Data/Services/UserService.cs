using Microsoft.EntityFrameworkCore;
using zBus.GLobal;
using zBus.Models;

namespace zBus.Data.Services
{
    public class UserService : IUserService
    {

        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Delete(string email)
        {
            var user= _context.Users.FirstOrDefault(x => x.Email == email);
            _context.Users.Remove(user!);
            _context.SaveChanges();
            GlobalVariables.Login_Status = false;
            GlobalVariables.User = String.Empty;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public User GetById(string email)
        {
            var user = _context.Users.FirstOrDefault(user => user.Email == email);
            return user!;
        }

        public void Update(string email, User user)
        {
            var old_user = _context.Users.FirstOrDefault(user => user.Email == email);
            old_user.Fisrt_name=user.Fisrt_name;
            old_user.Last_name=user.Last_name;
            old_user.PhotoPhath=user.PhotoPhath;
            old_user.Phone_number=user.Phone_number;    
            old_user.Address=user.Address;
            old_user.Password=user.Password;
            _context.Users.Update(old_user);
            _context.SaveChanges();

        }
        public void Update_Pass(string password)
        {
            var old_user = _context.Users.FirstOrDefault(user => user.Email == GlobalVariables.User);
            old_user.Password = password;
      
            _context.Users.Update(old_user);
            _context.SaveChanges();

        }


        public bool Exist(string mail)
        {

            bool test = _context.Users.Any(u => u.Email == mail);
            if (test)
                return true;
            else
                return false;
        }
    }

}
