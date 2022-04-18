using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ITenantDal
{
    Tenant Add(Tenant tenant);
    void Delete(long id);
    List<Tenant> GetByBusinessId(int businessId);
    Tenant GetByBusinessIdAndAccountId(int businessId, long accountId);
    Tenant GetById(long id);
    Tenant GetExtById(long id);
    List<Tenant> GetExtsByBusinessId(int businessId);
    void Update(Tenant tenant);
}
