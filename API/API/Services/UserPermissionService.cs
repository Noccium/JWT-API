using API.Extensions;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using System.Security;
using System.Security.Claims;

namespace API.Services
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly IUserPermissionRepository _userPermissionRepository;

        public UserPermissionService(IUserPermissionRepository permissionRepository)
        {
            _userPermissionRepository = permissionRepository;
        }

        public async Task<bool> HasPermission(ClaimsPrincipal user, string feature, string action)
        {
            var userId = user.GetUserId();
            var tenantId = user.GetTenantId();

            if (userId == null || tenantId == null)
            {
                return false;
            }

            // Consulta ao banco para verificar permissão
            return await _userPermissionRepository.UserHasPermission(userId, tenantId, feature, action);
        }
    }
}
