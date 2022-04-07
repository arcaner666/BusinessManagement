using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface ITenantBl
{
    IDataResult<List<TenantDto>> GetByBusinessId(int businessId);
}
