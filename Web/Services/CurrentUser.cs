using Application.Common.Interfaces;
using System.Security.Claims;

namespace Web.Services;

public class CurrentUser(IHttpContextAccessor httpContextAccessor) : IUser
{
    public string? Id => httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public IEnumerable<string>? Roles => httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(c => c.Value);
}
