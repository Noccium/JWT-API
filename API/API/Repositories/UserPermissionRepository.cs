using API.Model;
using API.Repositories.Interfaces;

namespace API.Repositories
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly IUserTenantRepository _userTenantRepository;
        private readonly IFeatureRepository _featureRepository;
        private readonly IOperationRepository _permissionRepository;
        private readonly List<UserPermission> _permissions;

        public UserPermissionRepository()
        {
            _featureRepository = new FeatureRepository();
            _permissionRepository = new OperationRepository();
            _userTenantRepository = new UserTenantRepository();
            _permissions = GetAll();
        }

        public List<UserPermission> Get(UserTenant userTenant)
        {
            return _permissions.Where(p => p.UserTenant?.Id == userTenant.Id).ToList();
        }

        public List<UserPermission> GetAll()
        {
            var feature = _featureRepository.Get("WeatherForecast");

            var get = _permissionRepository.Get("GET");
            var post = _permissionRepository.Get("POST");
            var put = _permissionRepository.Get("PUT");
            var delete = _permissionRepository.Get("DELETE");

            return
            [
                new (){ Id = 1, UserTenant = _userTenantRepository.Get(UserRepository.Vitor.Id, TenantRepository.Production.Id), Feature = feature, Action = get },
                new (){ Id = 2, UserTenant = _userTenantRepository.Get(UserRepository.Vitor.Id, TenantRepository.Staging.Id), Feature = feature, Action = get },

                new (){ Id = 3, UserTenant = _userTenantRepository.Get(UserRepository.Lucas.Id, TenantRepository.Production.Id), Feature = feature, Action = get },

                new (){ Id = 4, UserTenant = _userTenantRepository.Get(UserRepository.Felipe.Id, TenantRepository.Staging.Id), Feature = feature, Action = get },
            ];
        }

        public async Task<bool> UserHasPermission(int? userId, int? tenantId, string feature, string action)
        {
            var userTenant = _userTenantRepository.Get(userId.GetValueOrDefault(), tenantId.GetValueOrDefault());
            if (userTenant == null)
                return false;

            return  _permissions.FirstOrDefault(p => 
                p.UserTenant?.Id == userTenant.Id &&
                string.Equals(p.Feature?.Name, feature, StringComparison.OrdinalIgnoreCase) &&
                string.Equals(p.Action?.Name, action, StringComparison.OrdinalIgnoreCase)) != null;
        }
    }
}
