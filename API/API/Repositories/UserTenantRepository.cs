using API.Model;
using API.Repositories.Interfaces;
using System.Xml.Linq;

namespace API.Repositories
{
    public class UserTenantRepository : IUserTenantRepository
    {
        private readonly IUserRepository _userRepository;
        private readonly ITenantRepository _tenantRepository;
        private readonly List<UserTenant> _userTenants;

        public UserTenantRepository()
        {
            _userRepository = new UserRepository();
            _tenantRepository = new TenantRepository();
            _userTenants = GetAll();
        }

        public UserTenant? Get(int userId, int tenantId)
        {
            return _userTenants.FirstOrDefault(userTenant => userTenant.UserId == userId && userTenant.TenantId == tenantId);
        }

        public UserTenant? Get(string email, string password, string tenantName)
        {
            var user = _userRepository.Get(email, password);
            if (user == null)
                return null;

            var tenant = _tenantRepository.Get(tenantName);
            if (tenant == null)
                return null;

            return Get(user.Id, tenant.Id);
        }

        public List<UserTenant> GetAll()
        {
            return
            [
                new () { Id = 1, UserId = UserRepository.Vitor.Id, TenantId = TenantRepository.Production.Id },
                new () { Id = 2, UserId = UserRepository.Vitor.Id, TenantId = TenantRepository.Staging.Id },
                new () { Id = 3, UserId = UserRepository.Lucas.Id, TenantId = TenantRepository.Production.Id },
                new () { Id = 4, UserId = UserRepository.Felipe.Id, TenantId = TenantRepository.Staging.Id },
            ];
        }
    }
}
