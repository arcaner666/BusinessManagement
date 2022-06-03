using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Extensions;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.BusinessLayer.Utilities.Security.Encryption;
using BusinessManagement.Entities.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace BusinessManagement.BusinessLayer.Utilities.Security.JWT;

public class JwtHelper : ITokenService
{
    public IConfiguration Configuration { get; }
    private readonly TokenOptions _tokenOptions;
    private DateTime _accessTokenExpiration;

    public JwtHelper(IConfiguration configuration)
    {
        Configuration = configuration;
        _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
    }

    public string GenerateAccessToken(long systemUserId, IEnumerable<SystemUserClaimExtDto> systemUserClaimExtDtos)
    {
        _accessTokenExpiration = DateTime.Now.AddSeconds(_tokenOptions.AccessTokenExpiration);
        var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
        var jwt = CreateJwtSecurityToken(_tokenOptions, systemUserId, signingCredentials, systemUserClaimExtDtos);
        var tokenHandler = new JwtSecurityTokenHandler();
        var accessToken = tokenHandler.WriteToken(jwt);
        return accessToken;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public JwtSecurityToken CreateJwtSecurityToken(
        TokenOptions tokenOptions,
        long systemUserId,
        SigningCredentials signingCredentials,
        IEnumerable<SystemUserClaimExtDto> systemUserClaimExtDtos
    )
    {
        var jwt = new JwtSecurityToken(
            issuer: tokenOptions.Issuer,
            audience: tokenOptions.Audience,
            expires: _accessTokenExpiration,
            notBefore: DateTime.Now,
            claims: SetOperationClaims(systemUserId, systemUserClaimExtDtos),
            signingCredentials: signingCredentials
        );
        return jwt;
    }

    private List<Claim> SetOperationClaims(long systemUserId, IEnumerable<SystemUserClaimExtDto> systemUserClaimExtDtos)
    {
        var claims = new List<Claim>();
        claims.AddNameIdentifier(systemUserId.ToString());
        claims.AddRoles(systemUserClaimExtDtos.Select(p => p.OperationClaimName).ToArray());

        return claims;
    }

    public IDataResult<ClaimsPrincipal> GetPrincipalFromExpiredToken(string accessToken)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey),
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(accessToken, tokenValidationParameters, out SecurityToken securityToken);
        if (securityToken is not JwtSecurityToken jwt || !jwt.Header.Alg.Equals(SecurityAlgorithms.HmacSha512Signature, StringComparison.InvariantCultureIgnoreCase))
        {
            return new ErrorDataResult<ClaimsPrincipal>(Messages.AuthorizationCanNotGetClaimsPrincipal);
        }

        return new SuccessDataResult<ClaimsPrincipal>(principal);
    }
}
