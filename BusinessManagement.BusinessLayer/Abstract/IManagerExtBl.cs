using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IManagerExtBl
{
    IDataResult<IEnumerable<ManagerExtDto>> GetExtsByBusinessId(int businessId);
}
