using AutoMapper;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Constants;
using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Concrete;

public class TenantBl : ITenantBl
{
    private readonly IMapper _mapper;
    private readonly ITenantDal _tenantDal;

    public TenantBl(
        IMapper mapper,
        ITenantDal tenantDal
    )
    {
        _mapper = mapper;
        _tenantDal = tenantDal;
    }

    public IDataResult<TenantDto> Add(TenantDto tenantDto)
    {
        Tenant searchedTenant = _tenantDal.GetByBusinessIdAndAccountId(tenantDto.BusinessId, tenantDto.AccountId);
        if (searchedTenant is not null)
            return new ErrorDataResult<TenantDto>(Messages.TenantAlreadyExists);

        var addedTenant = _mapper.Map<Tenant>(tenantDto);

        addedTenant.CreatedAt = DateTimeOffset.Now;
        addedTenant.UpdatedAt = DateTimeOffset.Now;
        long id = _tenantDal.Add(addedTenant);
        addedTenant.TenantId = id;

        var addedTenantDto = _mapper.Map<TenantDto>(addedTenant);

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

    public IDataResult<TenantDto> GetByAccountId(long accountId)
    {
        Tenant tenant = _tenantDal.GetByAccountId(accountId);
        if (tenant is null)
            return new ErrorDataResult<TenantDto>(Messages.TenantNotFound);

        var tenantDto = _mapper.Map<TenantDto>(tenant);

        return new SuccessDataResult<TenantDto>(tenantDto, Messages.TenantListedByAccountId);
    }

    public IDataResult<List<TenantDto>> GetByBusinessId(int businessId)
    {
        List<Tenant> tenants = _tenantDal.GetByBusinessId(businessId);
        if (!tenants.Any())
            return new ErrorDataResult<List<TenantDto>>(Messages.TenantsNotFound);

        var tenantDtos = _mapper.Map<List<TenantDto>>(tenants);

        return new SuccessDataResult<List<TenantDto>>(tenantDtos, Messages.TenantsListedByBusinessId);
    }

    public IDataResult<TenantDto> GetById(long id)
    {
        Tenant tenant = _tenantDal.GetById(id);
        if (tenant is null)
            return new ErrorDataResult<TenantDto>(Messages.TenantNotFound);

        var tenantDto = _mapper.Map<TenantDto>(tenant);

        return new SuccessDataResult<TenantDto>(tenantDto, Messages.TenantListedById);
    }

    public IDataResult<TenantExtDto> GetExtByAccountId(long accountId)
    {
        TenantExt tenantExt = _tenantDal.GetExtByAccountId(accountId);
        if (tenantExt is null)
            return new ErrorDataResult<TenantExtDto>(Messages.TenantNotFound);

        var tenantExtDto = _mapper.Map<TenantExtDto>(tenantExt);

        return new SuccessDataResult<TenantExtDto>(tenantExtDto, Messages.TenantExtListedByAccountId);
    }

    public IDataResult<TenantExtDto> GetExtById(long id)
    {
        TenantExt tenantExt = _tenantDal.GetExtById(id);
        if (tenantExt is null)
            return new ErrorDataResult<TenantExtDto>(Messages.TenantNotFound);

        var tenantExtDto = _mapper.Map<TenantExtDto>(tenantExt);

        return new SuccessDataResult<TenantExtDto>(tenantExtDto, Messages.TenantExtListedById);
    }

    public IDataResult<List<TenantExtDto>> GetExtsByBusinessId(int businessId)
    {
        List<TenantExt> tenantExts = _tenantDal.GetExtsByBusinessId(businessId);
        if (!tenantExts.Any())
            return new ErrorDataResult<List<TenantExtDto>>(Messages.TenantsNotFound);
        
        var tenantExtDtos = _mapper.Map<List<TenantExtDto>>(tenantExts);

        return new SuccessDataResult<List<TenantExtDto>>(tenantExtDtos, Messages.TenantExtsListedByBusinessId);
    }

    public IResult Update(TenantDto tenantDto)
    {
        Tenant tenant = _tenantDal.GetById(tenantDto.TenantId);
        if (tenant is null)
            return new ErrorDataResult<TenantDto>(Messages.TenantNotFound);

        tenant.NameSurname = tenantDto.NameSurname;
        tenant.Email = tenantDto.Email;
        tenant.DateOfBirth = tenantDto.DateOfBirth;
        tenant.Gender = tenantDto.Gender;
        tenant.Notes = tenantDto.Notes;
        tenant.AvatarUrl = tenantDto.AvatarUrl;
        tenant.TaxOffice = tenantDto.TaxOffice;
        tenant.TaxNumber = tenantDto.TaxNumber;
        tenant.IdentityNumber = tenantDto.IdentityNumber;
        tenant.StandartMaturity = tenantDto.StandartMaturity;
        tenant.UpdatedAt = DateTimeOffset.Now;
        _tenantDal.Update(tenant);

        return new SuccessResult(Messages.TenantUpdated);
    }
}
