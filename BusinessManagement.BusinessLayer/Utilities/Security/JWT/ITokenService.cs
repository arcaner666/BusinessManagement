using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.ExtendedDatabaseModels;
using System.Security.Claims;

namespace BusinessManagement.BusinessLayer.Utilities.Security.JWT;

public interface ITokenService
{
    string GenerateAccessToken(long systemUserId, List<SystemUserClaimExtDto> systemUserClaimExtDtos);
    string GenerateRefreshToken();
    IDataResult<ClaimsPrincipal> GetPrincipalFromExpiredToken(string accessToken);
}
