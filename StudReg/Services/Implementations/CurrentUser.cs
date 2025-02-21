using System.Security.Claims;
using StudReg.Services.Interfaces;

namespace StudReg.Services.Implementations
{
    public class CurrentUser : ICurrentUser
    {
        private readonly HttpContextAccessor _http;

        public CurrentUser(HttpContextAccessor http)
        {
            _http = http;
        }

        public string GetCurrentUser()
        {
            return _http.HttpContext.User.FindFirst(ClaimTypes.Email).Value;
        }
    }
}
