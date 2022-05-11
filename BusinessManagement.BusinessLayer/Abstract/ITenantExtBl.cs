using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ITenantExtBl
{
    IResult AddExt(TenantExtDto tenantExtDto);
    IResult DeleteExt(long id);
    IResult DeleteExtByAccountId(long accountId);
    IDataResult<TenantExtDto> GetExtByAccountId(long accountId);
    IDataResult<TenantExtDto> GetExtById(long id);
    IDataResult<List<TenantExtDto>> GetExtsByBusinessId(int businessId);
    IResult UpdateExt(TenantExtDto tenantExtDto);
}
