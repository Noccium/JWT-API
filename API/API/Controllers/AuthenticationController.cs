using API.Dtos;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        public AuthenticationController(ITokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost(Name = "login")]
        public ActionResult Login(Login login)
        {
            var token = _tokenService.GenerateToken(login);

            if (string.IsNullOrEmpty(token))
                return Unauthorized();

            return Ok(token);
        }
    }
}
