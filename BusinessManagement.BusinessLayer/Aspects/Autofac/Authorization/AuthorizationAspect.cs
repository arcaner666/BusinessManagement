using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Extensions;
using BusinessManagement.BusinessLayer.Utilities.Interceptors;
using Castle.DynamicProxy;
using Microsoft.AspNetCore.Http;

namespace BusinessManagement.BusinessLayer.Aspects.Autofac.Authorization;

public class AuthorizationAspect : MethodInterception
{
    private readonly string[] _roles;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationAspect(string roles, IHttpContextAccessor httpContextAccessor)
    {
        _roles = roles.Split(',');
        _httpContextAccessor = httpContextAccessor;
    }

    protected override void OnBefore(IInvocation invocation)
    {
        var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
        foreach (var role in _roles)
        {
            if (roleClaims.Contains(role))
            {
                return;
            }
        }
        throw new Exception(Messages.AuthorizationDenied);
    }
}
