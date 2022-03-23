using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract
{
    public interface IManagerBl
    {
        IDataResult<ManagerDto> Add(ManagerDto managerDto);
    }
}
