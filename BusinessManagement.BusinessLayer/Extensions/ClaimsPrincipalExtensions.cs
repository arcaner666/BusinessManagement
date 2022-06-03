using System.Security.Claims;

namespace BusinessManagement.BusinessLayer.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static IEnumerable<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
        return result;
    }

    public static IEnumerable<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.Claims(ClaimTypes.Role);
    }

    public static IEnumerable<string> ClaimSystemUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.Claims(ClaimTypes.NameIdentifier);
    }
}
