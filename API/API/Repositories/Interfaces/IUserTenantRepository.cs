using API.Model;

namespace API.Repositories.Interfaces
{
    public interface IUserTenantRepository
    {
        List<UserTenant> GetAll();
        UserTenant? Get(int userId, int tenantId);
        UserTenant? Get(string email, string password, string tenantName);
    }
}