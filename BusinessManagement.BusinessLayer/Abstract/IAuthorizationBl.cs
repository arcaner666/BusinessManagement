using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IAuthorizationBl
{
    IResult LoginWithEmail(AuthorizationDto authorizationDto);
    IResult LoginWithPhone(AuthorizationDto authorizationDto);
    IResult Logout(long systemUserId);
    IResult RefreshAccessToken(AuthorizationDto authorizationDto);
    IResult RegisterSectionManager(RegisterSectionManagerDto registerSectionManagerDto);
}
