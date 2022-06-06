using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ITenantAdvBl
{
    IResult Add(TenantExtDto tenantExtDto);
    IResult Delete(long id);
    IResult DeleteByAccountId(long accountId);
    IResult Update(TenantExtDto tenantExtDto);
}
