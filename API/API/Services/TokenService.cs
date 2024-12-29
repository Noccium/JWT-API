using API.Dtos;
using API.Model;
using API.Repositories.Interfaces;
using API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserTenantRepository _userTenantRepository;
        private readonly IUserPermissionRepository _userTenantFeaturePermissionRepository;

        public TokenService(IConfiguration configuration, IUserTenantRepository userTenantRepository, IUserPermissionRepository userTenantFeaturePermissionRepository)
        {
            _configuration = configuration;
            _userTenantRepository = userTenantRepository;
            _userTenantFeaturePermissionRepository = userTenantFeaturePermissionRepository;
        }

        public string GenerateToken(Login login)
        {
            if (login == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(login.Email) ||
                string.IsNullOrEmpty(login.Password) ||
                 string.IsNullOrEmpty(login.Tenant))
            {
                return string.Empty;
            }

            var userTenant = _userTenantRepository.Get(login.Email, login.Password, login.Tenant);

            if (userTenant == null)
            {
                return string.Empty;
            }

            var tenantUserRoles = _userTenantFeaturePermissionRepository.Get(userTenant);
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var issuer = _configuration["Jwt:Issuer"];
            var audience = _configuration["Jwt:Audience"];

            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var tokenOptions = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims:
                [
                    new Claim(ClaimTypes.NameIdentifier, userTenant.UserId.ToString(CultureInfo.InvariantCulture)),
                    new Claim(ClaimTypes.Email, login.Email),
                    new Claim("tenantId", userTenant.TenantId.ToString(CultureInfo.InvariantCulture)),
                ],
                expires: DateTime.Now.AddHours(2),
                signingCredentials: signinCredentials);
            
            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
