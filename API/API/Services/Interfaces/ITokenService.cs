using API.Dtos;
namespace API.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Login login);
    }
}
