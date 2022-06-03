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
        TenantDto searchedTenantDto = _tenantDal.GetByBusinessIdAndAccountId(tenantDto.BusinessId, tenantDto.AccountId);
        if (searchedTenantDto is not null)
        {
            return new ErrorDataResult<TenantDto>(Messages.TenantAlreadyExists);
        }

        tenantDto.CreatedAt = DateTimeOffset.Now;
        tenantDto.UpdatedAt = DateTimeOffset.Now;
        long id = _tenantDal.Add(tenantDto);
        tenantDto.TenantId = id;

        return new SuccessDataResult<TenantDto>(tenantDto, Messages.TenantAdded);
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
        TenantDto tenantDto = _tenantDal.GetByAccountId(accountId);
        if (tenantDto is null)
            return new ErrorDataResult<TenantDto>(Messages.TenantNotFound);

        return new SuccessDataResult<TenantDto>(tenantDto, Messages.TenantListedByAccountId);
    }

    public IDataResult<IEnumerable<TenantDto>> GetByBusinessId(int businessId)
    {
        IEnumerable<TenantDto> tenantDtos = _tenantDal.GetByBusinessId(businessId);
        if (!tenantDtos.Any())
            return new ErrorDataResult<IEnumerable<TenantDto>>(Messages.TenantsNotFound);

        return new SuccessDataResult<IEnumerable<TenantDto>>(tenantDtos, Messages.TenantsListedByBusinessId);
    }

    public IDataResult<TenantDto> GetById(long id)
    {
        TenantDto tenantDto = _tenantDal.GetById(id);
        if (tenantDto is null)
            return new ErrorDataResult<TenantDto>(Messages.TenantNotFound);

        return new SuccessDataResult<TenantDto>(tenantDto, Messages.TenantListedById);
    }

    public IResult Update(TenantDto tenantDto)
    {
        var searchedTenantResult = GetById(tenantDto.TenantId);
        if (!searchedTenantResult.Success)
            return searchedTenantResult;

        searchedTenantResult.Data.NameSurname = tenantDto.NameSurname;
        searchedTenantResult.Data.Email = tenantDto.Email;
        searchedTenantResult.Data.DateOfBirth = tenantDto.DateOfBirth;
        searchedTenantResult.Data.Gender = tenantDto.Gender;
        searchedTenantResult.Data.Notes = tenantDto.Notes;
        searchedTenantResult.Data.AvatarUrl = tenantDto.AvatarUrl;
        searchedTenantResult.Data.TaxOffice = tenantDto.TaxOffice;
        searchedTenantResult.Data.TaxNumber = tenantDto.TaxNumber;
        searchedTenantResult.Data.IdentityNumber = tenantDto.IdentityNumber;
        searchedTenantResult.Data.StandartMaturity = tenantDto.StandartMaturity;
        searchedTenantResult.Data.UpdatedAt = DateTimeOffset.Now;
        _tenantDal.Update(searchedTenantResult.Data);

        return new SuccessResult(Messages.TenantUpdated);
    }
}
