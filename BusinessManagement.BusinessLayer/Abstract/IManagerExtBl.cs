using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IManagerExtBl
{
    IDataResult<List<ManagerExtDto>> GetExtsByBusinessId(int businessId);
}
