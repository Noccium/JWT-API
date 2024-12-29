using System.Security.Claims;

namespace API.Services.Interfaces
{
    public interface IUserPermissionService
    {
        Task<bool> HasPermission(ClaimsPrincipal user, string feature, string action);
    }
}