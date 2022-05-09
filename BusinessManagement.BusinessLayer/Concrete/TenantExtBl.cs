using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Aspects.Autofac.Transaction;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class TenantExtBl : ITenantExtBl
{
    private readonly IAccountBl _accountBl;
    private readonly IAccountGroupBl _accountGroupBl;
    private readonly IAccountTypeBl _accountTypeBl;
    private readonly ITenantBl _tenantBl;
    private readonly ITenantDal _tenantDal;

    public TenantExtBl(
        IAccountBl accountBl,
        IAccountGroupBl accountGroupBl,
        IAccountTypeBl accountTypeBl,
        ITenantBl tenantBl,
        ITenantDal tenantDal
    )
    {
        _accountBl = accountBl;
        _accountGroupBl = accountGroupBl;
        _accountTypeBl = accountTypeBl;
        _tenantBl = tenantBl;
        _tenantDal = tenantDal;
    }

    [TransactionScopeAspect]
    public IResult AddExt(TenantExtDto tenantExtDto)
    {
        // Kiracının hesap grubunun id'si getirilir.
        var getAccountGroupResult = _accountGroupBl.GetByAccountGroupCode("120");
        if (!getAccountGroupResult.Success)
            return getAccountGroupResult;

        // Kiracının hesap tipinin id'si getirilir.
        var getAccountTypeResult = _accountTypeBl.GetByAccountTypeName("Kiracı");
        if (!getAccountTypeResult.Success)
            return getAccountTypeResult;

        // Yeni bir cari hesap oluşturulur.
        AccountDto accountDto = new()
        {
            BusinessId = tenantExtDto.BusinessId,
            BranchId = tenantExtDto.BranchId,
            AccountGroupId = getAccountGroupResult.Data.AccountGroupId,
            AccountTypeId = getAccountTypeResult.Data.AccountTypeId,
            AccountOrder = tenantExtDto.AccountOrder,
            AccountName = tenantExtDto.AccountName,
            AccountCode = tenantExtDto.AccountCode,
            Limit = tenantExtDto.Limit,
        };
        var addAccountResult = _accountBl.Add(accountDto);
        if (!addAccountResult.Success)
            return addAccountResult;

        // Yeni bir kiracı eklenir.
        TenantDto addedTenantDto = new()
        {
            BusinessId = tenantExtDto.BusinessId,
            BranchId = tenantExtDto.BranchId,
            AccountId = addAccountResult.Data.AccountId,
            NameSurname = tenantExtDto.NameSurname,
            Email = tenantExtDto.Email,
            Phone = tenantExtDto.Phone,
            DateOfBirth = tenantExtDto.DateOfBirth,
            Gender = tenantExtDto.Gender,
            Notes = tenantExtDto.Notes,
            AvatarUrl = tenantExtDto.AvatarUrl,
            TaxOffice = tenantExtDto.TaxOffice,
            TaxNumber = tenantExtDto.TaxNumber,
            IdentityNumber = tenantExtDto.IdentityNumber,
            StandartMaturity = tenantExtDto.StandartMaturity,
        };
        var addTenantResult = _tenantBl.Add(addedTenantDto);
        if (!addTenantResult.Success)
            return addTenantResult;

        return new SuccessResult(Messages.TenantExtAdded);
    }

    [TransactionScopeAspect]
    public IResult DeleteExt(long id)
    {
        var searchedTenantResult = _tenantBl.GetById(id);
        if (!searchedTenantResult.Success)
            return searchedTenantResult;

        var deleteTenantResult = _tenantBl.Delete(searchedTenantResult.Data.TenantId);
        if (!deleteTenantResult.Success)
            return deleteTenantResult;

        var deleteAccountResult = _accountBl.Delete(searchedTenantResult.Data.AccountId);
        if (!deleteAccountResult.Success)
            return deleteAccountResult;

        return new SuccessResult(Messages.TenantExtDeleted);
    }

    public IDataResult<TenantExtDto> GetExtById(long id)
    {
        Tenant searchedTenant = _tenantDal.GetExtById(id);
        if (searchedTenant is null)
            return new ErrorDataResult<TenantExtDto>(Messages.TenantNotFound);

        TenantExtDto searchedTenantExtDto = FillExtDto(searchedTenant);

        return new SuccessDataResult<TenantExtDto>(searchedTenantExtDto, Messages.TenantExtListedById);
    }

    public IDataResult<List<TenantExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<Tenant> searchedTenants = _tenantDal.GetExtsByBusinessId(businessId);
        if (searchedTenants.Count == 0)
            return new ErrorDataResult<List<TenantExtDto>>(Messages.TenantsNotFound);

        List<TenantExtDto> searchedTenantExtDtos = FillExtDtos(searchedTenants);

        return new SuccessDataResult<List<TenantExtDto>>(searchedTenantExtDtos, Messages.TenantExtsListedByBusinessId);
    }

    [TransactionScopeAspect]
    public IResult UpdateExt(TenantExtDto tenantExtDto)
    {
        // Kiracının cari hesabı güncellenir.
        AccountDto updatedAccountDto = new()
        {
            AccountId = tenantExtDto.AccountId,
            AccountName = tenantExtDto.AccountName,
            Limit = tenantExtDto.Limit,
         };
        var updateAccountResult = _accountBl.Update(updatedAccountDto);
        if (!updateAccountResult.Success)
            return updateAccountResult;

        // Kiracı güncellenir.
        TenantDto updatedTenantDto = new()
        {
            TenantId = tenantExtDto.TenantId,
            NameSurname = tenantExtDto.NameSurname,
            Email = tenantExtDto.Email,
            DateOfBirth = tenantExtDto.DateOfBirth,
            Gender = tenantExtDto.Gender,
            Notes = tenantExtDto.Notes,
            AvatarUrl = tenantExtDto.AvatarUrl,
            TaxOffice = tenantExtDto.TaxOffice,
            TaxNumber = tenantExtDto.TaxNumber,
            IdentityNumber = tenantExtDto.IdentityNumber,
            StandartMaturity = tenantExtDto.StandartMaturity,
        };
        var updateTenantResult = _tenantBl.Update(updatedTenantDto);
        if (!updateTenantResult.Success)
            return updateTenantResult;

        return new SuccessResult(Messages.TenantExtUpdated);
    }

    private TenantExtDto FillExtDto(Tenant tenant)
    {
        TenantExtDto tenantExtDto = new()
        {
            TenantId = tenant.TenantId,
            BusinessId = tenant.BusinessId,
            BranchId = tenant.BranchId,
            AccountId = tenant.AccountId,
            NameSurname = tenant.NameSurname,
            Email = tenant.Email,
            Phone = tenant.Phone,
            DateOfBirth = tenant.DateOfBirth,
            Gender = tenant.Gender,
            Notes = tenant.Notes,
            AvatarUrl = tenant.AvatarUrl,
            TaxOffice = tenant.TaxOffice,
            TaxNumber = tenant.TaxNumber,
            IdentityNumber = tenant.IdentityNumber,
            StandartMaturity = tenant.StandartMaturity,
            CreatedAt = tenant.CreatedAt,
            UpdatedAt = tenant.UpdatedAt,

            // Extended With Account
            AccountGroupId = tenant.Account.AccountGroupId,
            AccountOrder = tenant.Account.AccountOrder,
            AccountName = tenant.Account.AccountName,
            AccountCode = tenant.Account.AccountCode,
            Limit = tenant.Account.Limit,
        };
        return tenantExtDto;
    }

    private List<TenantExtDto> FillExtDtos(List<Tenant> tenants)
    {
        List<TenantExtDto> tenantExtDtos = tenants.Select(tenant => FillExtDto(tenant)).ToList();

        return tenantExtDtos;
    }
}
