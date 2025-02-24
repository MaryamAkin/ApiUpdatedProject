using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAttribute : Attribute, IAuthorizationFilter
{
    private const string API_KEY_HEADER = "X-Api-Key";
    private const string VALID_API_KEY = "your-secret-api-key";

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(API_KEY_HEADER, out var extractedApiKey))
        {
            context.Result = new UnauthorizedResult(); // 401 Unauthorized
            return;
        }

        if (!extractedApiKey.ToString().Equals(VALID_API_KEY, StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new ForbidResult(); // 403 Forbidden
            return;
        }

    }
}
