using BusinessManagement.Entities.DatabaseModels;

namespace BusinessManagement.DataAccessLayer.Abstract;

public interface ITenantDal
{
    Tenant Add(Tenant tenant);
    void Delete(long id);
    List<Tenant> GetByBusinessId(int businessId);
    Tenant GetById(long id);
    List<Tenant> GetExtsByBusinessId(int businessId);
    Tenant GetIfAlreadyExist(int businessId, long accountId);
    void Update(Tenant tenant);
}
