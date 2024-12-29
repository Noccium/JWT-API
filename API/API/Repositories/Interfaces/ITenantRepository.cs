using API.Model;

namespace API.Repositories.Interfaces
{
    public interface ITenantRepository
    {
        Tenant? Get(int id);
        Tenant? Get(string name);
        List<Tenant> GetAll();
    }
}