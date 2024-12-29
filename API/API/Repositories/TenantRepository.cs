using API.Model;
using API.Repositories.Interfaces;

namespace API.Repositories
{
    public class TenantRepository : ITenantRepository
    {
        public static readonly Tenant Production = new() { Id = 1, Name = "Production" };
        public static readonly Tenant Staging = new() { Id = 2, Name = "Staging" };
        private readonly List<Tenant> _tenants =
        [
            Production, Staging
        ];

        public Tenant? Get(int id)
        {
            return _tenants.FirstOrDefault(t => t.Id == id);
        }

        public Tenant? Get(string name)
        {
            return _tenants.FirstOrDefault(t => string.Equals(t.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        public List<Tenant> GetAll()
        {
            return _tenants.ToList();
        }
    }
}
