using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Extensions;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.BusinessLayer.Utilities.Security.Hashing;
using BusinessManagement.BusinessLayer.Utilities.Security.JWT;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class AuthorizationBl : IAuthorizationBl
{
    private readonly IBranchBl _branchBl;
    private readonly IBusinessBl _businessBl;
    private readonly ICashExtBl _cashExtBl;
    private readonly ICurrencyBl _currencyBl;
    private readonly IFullAddressBl _fullAddressBl;
    private readonly IManagerBl _managerBl;
    private readonly IOperationClaimBl _operationClaimBl;
    private readonly ISectionGroupBl _sectionGroupBl;
    private readonly ISystemUserBl _systemUserBl;
    private readonly ISystemUserClaimBl _systemUserClaimBl;
    private readonly ITokenService _tokenHelper;

    public AuthorizationBl(
        IBranchBl branchBl,
        IBusinessBl businessBl,
        ICashExtBl cashExtBl,
        ICurrencyBl currencyBl,
        IFullAddressBl fullAddressBl,
        IManagerBl managerBl,
        IOperationClaimBl operationClaimBl,
        ISectionGroupBl sectionGroupBl,
        ISystemUserBl systemUserBl,
        ISystemUserClaimBl systemUserClaimBl,
        ITokenService tokenHelper
    )
    {
        _branchBl = branchBl;
        _businessBl = businessBl;
        _cashExtBl = cashExtBl;
        _currencyBl = currencyBl;
        _fullAddressBl = fullAddressBl;
        _managerBl = managerBl;
        _operationClaimBl = operationClaimBl;
        _sectionGroupBl = sectionGroupBl;
        _systemUserBl = systemUserBl;
        _systemUserClaimBl = systemUserClaimBl;
        _tokenHelper = tokenHelper;
    }

    public IResult LoginWithEmail(AuthorizationDto authorizationDto)
    {
        // Veri tabanında gönderilen e-posta adresine sahip biri var mı kontrol eder.
        var getSystemUserResult = _systemUserBl.GetByEmail(authorizationDto.Email);
        if (!getSystemUserResult.Success) 
            return getSystemUserResult;

        // Varsa giriş yapmaya çalışır.
        var loginResult = Login(authorizationDto, getSystemUserResult.Data);

        return loginResult;
    }

    public IResult LoginWithPhone(AuthorizationDto authorizationDto)
    {
        // Veri tabanında gönderilen telefon numarasına sahip biri var mı kontrol eder.
        var getSystemUserResult = _systemUserBl.GetByPhone(authorizationDto.Phone);
        if (!getSystemUserResult.Success) 
            return getSystemUserResult;

        // Varsa giriş yapmaya çalışır.
        var loginResult = Login(authorizationDto, getSystemUserResult.Data);

        return loginResult;
    }

    public IResult Logout(long systemUserId)
    {
        var getSystemUserResult = _systemUserBl.GetById(systemUserId);
        if (!getSystemUserResult.Success)
            return getSystemUserResult;

        getSystemUserResult.Data.RefreshToken = "";
        getSystemUserResult.Data.RefreshTokenExpiryTime = DateTime.Now;
        var updateSystemUserResult = _systemUserBl.Update(getSystemUserResult.Data);
        if (!updateSystemUserResult.Success)
            return updateSystemUserResult;

        return new SuccessResult(Messages.AuthorizationLoggedOut);
    }

    public IResult RefreshAccessToken(AuthorizationDto authorizationDto)
    {
        var getClaimsPrincipalResult = _tokenHelper.GetPrincipalFromExpiredToken(authorizationDto.AccessToken);
        if (!getClaimsPrincipalResult.Success)
            return getClaimsPrincipalResult;

        List<string> claimRoles = getClaimsPrincipalResult.Data.ClaimRoles();
        List<SystemUserClaimExtDto> systemUserClaimExtDtos = new();
        claimRoles.ForEach(role => systemUserClaimExtDtos.Add(
            new SystemUserClaimExtDto
            {
                OperationClaimName = role
            }
        ));

        long systemUserId = Convert.ToInt32(getClaimsPrincipalResult.Data.ClaimSystemUserId().FirstOrDefault());
        var getSystemUserResult = _systemUserBl.GetById(systemUserId);
        if (!getSystemUserResult.Success)
            return getSystemUserResult;
        if (getSystemUserResult.Data.RefreshToken != authorizationDto.RefreshToken)
            return new ErrorResult(Messages.AuthorizationTokenInvalid);
        if (getSystemUserResult.Data.RefreshTokenExpiryTime <= DateTime.Now)
            return new ErrorResult(Messages.AuthorizationTokenExpired);

        string newAccessToken = _tokenHelper.GenerateAccessToken(getSystemUserResult.Data.SystemUserId, systemUserClaimExtDtos);

        AuthorizationDto authorizationResponse = new()
        {
            AccessToken = newAccessToken,
        };

        return new SuccessDataResult<AuthorizationDto>(authorizationResponse, Messages.AuthorizationTokensRefreshed);
    }

    [TransactionScopeAspect]
    public IResult RegisterSectionManager(RegisterSectionManagerDto registerSectionManagerDto)
    {
        // Yeni bir sistem kullanıcısı eklenir.
        SystemUserDto systemUserDto = new()
        {
            Phone = registerSectionManagerDto.Phone,
            Role = "Manager",
        };
        var addSystemUserResult = _systemUserBl.Add(systemUserDto);
        if (!addSystemUserResult.Success) 
            return addSystemUserResult;

        // Yetki adından yetkinin id'si bulunur.
        var getOperationClaimResult = _operationClaimBl.GetByOperationClaimName("Manager");
        if (!getOperationClaimResult.Success) 
            return getOperationClaimResult;

        // Yönetici yetkileri verilir.
        SystemUserClaimDto systemUserClaimDto = new()
        {
            SystemUserId = addSystemUserResult.Data.SystemUserId,
            OperationClaimId = getOperationClaimResult.Data.OperationClaimId,
        };
        var addSystemUserClaimResult = _systemUserClaimBl.Add(systemUserClaimDto);
        if (!addSystemUserClaimResult.Success) 
            return addSystemUserClaimResult;

        // Yeni bir işletme eklenir.
        BusinessDto businessDto = new()
        {
            OwnerSystemUserId = addSystemUserResult.Data.SystemUserId,
            BusinessName = registerSectionManagerDto.BusinessName,
        };
        var addBusinessResult = _businessBl.Add(businessDto);
        if (!addBusinessResult.Success) 
            return addBusinessResult;

        // İşletmenin merkez şubesinin adresi eklenir.
        FullAddressDto fullAddressDto = new()
        {
            CityId = registerSectionManagerDto.CityId,
            DistrictId = registerSectionManagerDto.DistrictId,
            AddressTitle = "Merkez",
            PostalCode = 0,
            AddressText = registerSectionManagerDto.AddressText,
        };
        var addFullAddressResult = _fullAddressBl.Add(fullAddressDto);
        if (!addFullAddressResult.Success) 
            return addFullAddressResult;

        // İşletmenin merkez şubesi eklenir.
        BranchDto branchDto = new()
        {
            BusinessId = addBusinessResult.Data.BusinessId,
            FullAddressId = addFullAddressResult.Data.FullAddressId,
            BranchOrder = 1,
            BranchName = "Merkez",
            BranchCode = "000001",
        };
        var addBranchResult = _branchBl.Add(branchDto);
        if (!addBranchResult.Success) 
            return addBranchResult;

        // Kullanıcı kaydındaki işletme ve şube id'leri güncellenir.
        addSystemUserResult.Data.BusinessId = addBusinessResult.Data.BusinessId;
        addSystemUserResult.Data.BranchId = addBranchResult.Data.BranchId;
        var updateSystemUserResult = _systemUserBl.Update(addSystemUserResult.Data);
        if (!updateSystemUserResult.Success) 
            return updateSystemUserResult;

        // Kasanın doviz cinsi getirilir.
        var getCurrencyResult = _currencyBl.GetByCurrencyName("TL");
        if (!getCurrencyResult.Success)
            return getCurrencyResult;

        // İşletmenin kasası oluşturulur.
        CashExtDto cashExtDto = new()
        {
            BusinessId = addBusinessResult.Data.BusinessId,
            BranchId = addBranchResult.Data.BranchId,
            CurrencyId = getCurrencyResult.Data.CurrencyId,

            TaxOffice = registerSectionManagerDto.TaxOffice,
            TaxNumber = registerSectionManagerDto.TaxNumber,
            IdentityNumber = registerSectionManagerDto.IdentityNumber,
            AccountOrder = 1,
            AccountName = "TL Kasası",
            AccountCode = "10000000100000001",
            Limit = 0,
            StandartMaturity = 0,
        };
        var addCashExtResult = _cashExtBl.AddExt(cashExtDto);
        if (!addCashExtResult.Success)
            return addCashExtResult;

        // Yeni bir yönetici eklenir.
        ManagerDto managerDto = new()
        {
            BusinessId = addBusinessResult.Data.BusinessId,
            BranchId = addBranchResult.Data.BranchId,
            NameSurname = registerSectionManagerDto.NameSurname,
            Phone = registerSectionManagerDto.Phone,
        };
        var addManagerResult = _managerBl.Add(managerDto);
        if (!addManagerResult.Success) 
            return addManagerResult;

        // Yeni site grubu eklenir.
        SectionGroupDto sectionGroupDto = new()
        {
            BusinessId = addBusinessResult.Data.BusinessId,
            BranchId = addBranchResult.Data.BranchId,
            SectionGroupName = "Genel",
        };
        var addSectionGroupResult = _sectionGroupBl.Add(sectionGroupDto);
        if (!addSectionGroupResult.Success) 
            return addSectionGroupResult;

        return new SuccessResult(Messages.AuthorizationSectionManagerRegistered);
    }

    private IResult Login(AuthorizationDto authorizationDto, SystemUserDto systemUserDto)
    {
        // Şifre kontrol edilir.
        if (!HashingHelper.VerifyPasswordHash(authorizationDto.Password, systemUserDto.PasswordHash, systemUserDto.PasswordSalt))
            return new ErrorDataResult<AuthorizationDto>(Messages.AuthorizationWrongPassword);

        // Giriş yapan kullanıcının yetkileri getirilir.
        var getSystemUserClaimExtResult = _systemUserClaimBl.GetExtsBySystemUserId(systemUserDto.SystemUserId);
        if (!getSystemUserClaimExtResult.Success)
            return getSystemUserClaimExtResult;

        // Giriş yapan kullanıcı için access token ve refresh token üretilir.
        string accessToken = _tokenHelper.GenerateAccessToken(systemUserDto.SystemUserId, getSystemUserClaimExtResult.Data);
        string refreshToken = _tokenHelper.GenerateRefreshToken();

        // Arayüzden gönderilen bilgilere göre üretilen tokenlar veri tabanına kaydedilir.
        systemUserDto.RefreshToken = refreshToken;
        systemUserDto.RefreshTokenExpiryTime = DateTime.Now.AddSeconds(authorizationDto.RefreshTokenDuration);
        systemUserDto.UpdatedAt = DateTimeOffset.Now;
        var updateSystemUserResult = _systemUserBl.Update(systemUserDto);
        if (!updateSystemUserResult.Success) 
            return updateSystemUserResult;

        AuthorizationDto authorizationDtoResponse = new()
        {
            SystemUserId = systemUserDto.SystemUserId,
            Email = systemUserDto.Email,
            Phone = systemUserDto.Phone,
            Role = systemUserDto.Role,
            BusinessId = systemUserDto.BusinessId,
            BranchId = systemUserDto.BranchId,
            Blocked = systemUserDto.Blocked,
            RefreshToken = systemUserDto.RefreshToken,
            RefreshTokenExpiryTime = systemUserDto.RefreshTokenExpiryTime,
            CreatedAt = systemUserDto.CreatedAt,
            UpdatedAt = systemUserDto.UpdatedAt,
            AccessToken = accessToken,
        };

        return new SuccessDataResult<AuthorizationDto>(authorizationDtoResponse, Messages.AuthorizationLoggedIn);
    }
}
