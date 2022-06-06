using BusinessManagement.BusinessLayer.Utilities.Results;
using BusinessManagement.Entities.DTOs;

namespace BusinessManagement.BusinessLayer.Abstract;

public interface IEmployeeTypeBl
{
    IDataResult<List<EmployeeTypeDto>> GetAll();
}
