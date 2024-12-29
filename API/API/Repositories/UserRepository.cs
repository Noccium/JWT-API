using API.Model;
using API.Repositories.Interfaces;
using System.Xml.Linq;

namespace API.Repositories
{
    public class UserRepository : IUserRepository
    {
        public static readonly User Vitor = new() { Id = 1, Email = "vitor@email.com", Password = "password1" };
        public static readonly User Lucas = new() { Id = 2, Email = "lucas@email.com", Password = "password2" };
        public static readonly User Felipe = new() { Id = 3, Email = "felipe@email.com", Password = "password3" };

        private readonly List<User> _users =
        [
            Vitor, Lucas, Felipe
        ];

        public User? Get(string email, string password)
        {
            return _users.FirstOrDefault(u => email == u.Email && password == u.Password);
        }

        public List<User> GetAll()
        {
            return _users;
        }
    }
}
