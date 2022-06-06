using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ITenantBl
{
    IDataResult<TenantDto> Add(TenantDto tenantDto);
    IResult Delete(long id);
    IDataResult<TenantDto> GetByAccountId(long accountId);
    IDataResult<List<TenantDto>> GetByBusinessId(int businessId);
    IDataResult<TenantDto> GetById(long id);
    IDataResult<TenantExtDto> GetExtByAccountId(long accountId);
    IDataResult<TenantExtDto> GetExtById(long id);
    IDataResult<List<TenantExtDto>> GetExtsByBusinessId(int businessId);
    IResult Update(TenantDto tenantDto);
}
