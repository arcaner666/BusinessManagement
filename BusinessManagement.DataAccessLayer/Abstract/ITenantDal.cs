using BusinessManagement.Entities.DatabaseModels;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ITenantDal
{
    long Add(TenantDto tenantDto);
    void Delete(long id);
    TenantDto GetByAccountId(long accountId);
    IEnumerable<TenantDto> GetByBusinessId(int businessId);
    TenantDto GetByBusinessIdAndAccountId(int businessId, long accountId);
    TenantDto GetById(long id);
    TenantExtDto GetExtByAccountId(long accountId);
    TenantExtDto GetExtById(long id);
    IEnumerable<TenantExtDto> GetExtsByBusinessId(int businessId);
    void Update(TenantDto tenantDto);
}
