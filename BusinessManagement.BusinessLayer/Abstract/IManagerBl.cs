using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;
using BusinessManagement.Entities.ExtendedDatabaseModels;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IManagerBl
{
    IDataResult<ManagerDto> Add(ManagerDto managerDto);
    IDataResult<List<ManagerDto>> GetByBusinessId(int businessId);
    IDataResult<List<ManagerExtDto>> GetExtsByBusinessId(int businessId);
}
