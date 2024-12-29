using API.Attributes;
using API.Services.Interfaces;

namespace API.Handlers
{
    public class PermissionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IUserPermissionService permissionService)
        {
            var controller = context.Request.RouteValues["controller"]?.ToString() ?? string.Empty;

            if (controller != "Authentication")
            {
                var user = context.User;
                var action = context.Request.RouteValues["action"]?.ToString() ?? string.Empty;
                var hasPermission = await permissionService.HasPermission(user, controller, action);
                if (!hasPermission)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return;
                }
            }

            await _next(context);
        }
    }
}
