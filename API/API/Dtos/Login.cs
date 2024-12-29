using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Dtos
{
    public class Login
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Tenant { get; set; }
    }
}
