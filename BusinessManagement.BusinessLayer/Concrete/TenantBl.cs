using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Concrete;

public class TenantBl : ITenantBl
{
    private readonly ITenantDal _tenantDal;

    public TenantBl(
        ITenantDal tenantDal
    )
    {
        _tenantDal = tenantDal;
    }

    public IDataResult<TenantDto> Add(TenantDto tenantDto)
    {
        Tenant searchedTenant = _tenantDal.GetByBusinessIdAndAccountId(tenantDto.BusinessId, tenantDto.AccountId);
        if (searchedTenant is not null)
        {
            return new ErrorDataResult<TenantDto>(Messages.TenantAlreadyExists);
        }

        Tenant addedTenant = new()
        {
            BusinessId = tenantDto.BusinessId,
            BranchId = tenantDto.BranchId,
            AccountId = tenantDto.AccountId,
            NameSurname = tenantDto.NameSurname,
            Email = tenantDto.Email,
            Phone = tenantDto.Phone,
            DateOfBirth = tenantDto.DateOfBirth,
            Gender = tenantDto.Gender,
            Notes = tenantDto.Notes,
            AvatarUrl = tenantDto.AvatarUrl,
            TaxOffice = tenantDto.TaxOffice,
            TaxNumber = tenantDto.TaxNumber,
            IdentityNumber = tenantDto.IdentityNumber,
            StandartMaturity = tenantDto.StandartMaturity,
            CreatedAt = DateTimeOffset.Now,
            UpdatedAt = DateTimeOffset.Now,
        };
        _tenantDal.Add(addedTenant);

        TenantDto addedTenantDto = FillDto(addedTenant);

        return new SuccessDataResult<TenantDto>(addedTenantDto, Messages.TenantAdded);
    }

    public IResult Delete(long id)
    {
        var getTenantResult = GetById(id);
        if (getTenantResult is null)
            return getTenantResult;

        _tenantDal.Delete(id);

        return new SuccessResult(Messages.TenantDeleted);
    }

    public IDataResult<List<TenantDto>> GetByBusinessId(int businessId)
    {
        List<Tenant> searchedTenants = _tenantDal.GetByBusinessId(businessId);
        if (searchedTenants.Count == 0)
            return new ErrorDataResult<List<TenantDto>>(Messages.TenantsNotFound);

        List<TenantDto> searchedTenantDtos = FillDtos(searchedTenants);

        return new SuccessDataResult<List<TenantDto>>(searchedTenantDtos, Messages.TenantsListedByBusinessId);
    }

    public IDataResult<TenantDto> GetById(long id)
    {
        Tenant searchedTenant = _tenantDal.GetById(id);
        if (searchedTenant is null)
            return new ErrorDataResult<TenantDto>(Messages.TenantNotFound);

        TenantDto searchedTenantDto = FillDto(searchedTenant);

        return new SuccessDataResult<TenantDto>(searchedTenantDto, Messages.TenantListedById);
    }

    public IResult Update(TenantDto tenantDto)
    {
        Tenant searchedTenant = _tenantDal.GetById(tenantDto.TenantId);
        if (searchedTenant is null)
            return new ErrorDataResult<AccountDto>(Messages.TenantNotFound);

        searchedTenant.NameSurname = tenantDto.NameSurname;
        searchedTenant.Email = tenantDto.Email;
        searchedTenant.DateOfBirth = tenantDto.DateOfBirth;
        searchedTenant.Gender = tenantDto.Gender;
        searchedTenant.Notes = tenantDto.Notes;
        searchedTenant.AvatarUrl = tenantDto.AvatarUrl;
        searchedTenant.TaxOffice = tenantDto.TaxOffice;
        searchedTenant.TaxNumber = tenantDto.TaxNumber;
        searchedTenant.IdentityNumber = tenantDto.IdentityNumber;
        searchedTenant.StandartMaturity = tenantDto.StandartMaturity;
        searchedTenant.UpdatedAt = DateTimeOffset.Now;
        _tenantDal.Update(searchedTenant);

        return new SuccessResult(Messages.TenantUpdated);
    }

    private TenantDto FillDto(Tenant tenant)
    {
        TenantDto tenantDto = new()
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
        };

        return tenantDto;
    }

    private List<TenantDto> FillDtos(List<Tenant> tenants)
    {
        List<TenantDto> tenantDtos = tenants.Select(tenant => FillDto(tenant)).ToList();

        return tenantDtos;
    }
}
