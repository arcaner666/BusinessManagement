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

    public IDataResult<List<TenantDto>> GetByBusinessId(int businessId)
    {
        List<Tenant> searchedTenants = _tenantDal.GetByBusinessId(businessId);
        if (searchedTenants.Count == 0)
            return new ErrorDataResult<List<TenantDto>>(Messages.TenantsNotFound);

        List<TenantDto> searchedTenantDtos = FillDtos(searchedTenants);

        return new SuccessDataResult<List<TenantDto>>(searchedTenantDtos, Messages.TenantsListedByBusinessId);
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
            CreatedAt = tenant.CreatedAt,
            UpdatedAt = tenant.UpdatedAt,
        };
        return tenantExtDto;
    }

    private List<TenantExtDto> FillExtDtos(List<Tenant> tenants)
    {
        List<TenantExtDto> tenantExtDtos = tenants.Select(tenant => FillExtDto(tenant)).ToList();

        return tenantExtDtos;
    }
}
