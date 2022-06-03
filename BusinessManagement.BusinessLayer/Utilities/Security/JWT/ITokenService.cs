using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;
using System.Security.Claims;

namespace BusinessManagement.BusinessLayer.Utilities.Security.JWT;

public interface ITokenService
{
    string GenerateAccessToken(long systemUserId, IEnumerable<SystemUserClaimExtDto> systemUserClaimExtDtos);
    string GenerateRefreshToken();
    IDataResult<ClaimsPrincipal> GetPrincipalFromExpiredToken(string accessToken);
}
