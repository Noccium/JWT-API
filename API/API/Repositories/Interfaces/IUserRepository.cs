using API.Model;

namespace API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public User? Get(string email, string password);
        List<User> GetAll();
    }
}
