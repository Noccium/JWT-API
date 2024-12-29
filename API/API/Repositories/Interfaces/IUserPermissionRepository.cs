using API.Model;

namespace API.Repositories.Interfaces
{
    public interface IUserPermissionRepository
    {
        List<UserPermission> Get(UserTenant userTenant);
        List<UserPermission> GetAll();
        Task<bool> UserHasPermission(int? userId, int? tenantId, string feature, string action);
    }
}